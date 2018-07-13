using System;
using System.Collections.Generic;
using Saffron;
using Saffron.Randomization;
using Saffron.Serialization;

namespace Davfalcon.Engine.Combat
{
	public class CombatEvaluator : ICombatEvaluator
	{
		private IEffectsRegistry effects;

		public event BuffEventHandler OnBuffApplied;
		public event DamageEventHandler OnDamageTaken;

		private IList<ILogEntry> ApplyEffects(IEffectSource source, IUnit target, IUnit originator)
			=> effects.ApplyEffects(source, target, originator);

		private IList<ILogEntry> ApplyEffects(IEffectSource source, IUnit target, IUnit originator, int value)
			=> effects.ApplyEffects(source, target, originator, value);

		private void AdjustHPMP(IUnit unit, int prevMaxHP, int prevMaxMP)
		{
			unit.CombatProperties.CurrentHP += unit.Stats[CombatStats.HP] - prevMaxHP;
			unit.CombatProperties.CurrentMP += unit.Stats[CombatStats.MP] - prevMaxMP;
		}

		public void ApplyBuff(IUnit unit, IBuff buff, string source = null)
		{
			int maxHP = unit.Stats[CombatStats.HP];
			int maxMP = unit.Stats[CombatStats.MP];

			IBuff b = (IBuff)Serializer.DeepClone(buff);
			b.Source = source;
			b.Reset();
			unit.CombatProperties.Buffs.Add(b);

			AdjustHPMP(unit, maxHP, maxMP);

			OnBuffApplied?.Invoke(unit, buff);
		}

		public void RemoveBuff(IUnit unit, IBuff buff)
		{
			int maxHP = unit.Stats[CombatStats.HP];
			int maxMP = unit.Stats[CombatStats.MP];

			unit.CombatProperties.Buffs.Remove(buff);

			AdjustHPMP(unit, maxHP, maxMP);
		}

		public void Initialize(IUnit unit)
		{
			// Set HP/MP to max values
			unit.CombatProperties.CurrentHP = unit.Stats[CombatStats.HP];
			unit.CombatProperties.CurrentMP = unit.Stats[CombatStats.MP];

			// Apply buffs granted by equipment
			foreach (IEquipment equip in unit.ItemProperties.Equipment)
			{
				foreach (IBuff buff in equip.GrantedBuffs)
				{
					ApplyBuff(unit, buff, String.Format("{0}'s {1}", unit.Name, equip.Name));
				}
			}
		}

		public void Cleanup(IUnit unit)
		{
			// Reset HP/MP to 0
			unit.CombatProperties.CurrentHP = 0;
			unit.CombatProperties.CurrentMP = 0;

			// Clear all buffs/debuffs
			unit.CombatProperties.Buffs.Clear();
		}

		public IList<ILogEntry> Upkeep(IUnit unit)
		{
			List<ILogEntry> effects = new List<ILogEntry>();
			List<IBuff> expired = new List<IBuff>();

			foreach (IBuff buff in unit.CombatProperties.Buffs)
			{
				// Apply repeating effects
				if (buff.Duration > 0 && buff.Remaining > 0 ||
					buff.Duration == 0)
					effects.AddRange(ApplyEffects(buff, unit, unit));

				// Tick buff timers
				buff.Tick();

				// Record expired buffs (cannot remove during enumeration)
				if (buff.Duration > 0 && buff.Remaining == 0)
					expired.Add(buff);
			}

			// Remove expired buffs
			foreach (IBuff buff in expired)
			{
				RemoveBuff(unit, buff);
			}

			return effects;
		}

		public int ScaleDamageValue(int baseValue, int scaling)
		{
			return baseValue.Scale(scaling);
		}

		public int MitigateDamageValue(int incomingValue, int resistance)
		{
			return incomingValue.Scale(-resistance);
		}

		public int ChangeHP(IUnit unit, int amount)
		{
			int initial = unit.CombatProperties.CurrentHP;
			unit.CombatProperties.CurrentHP = (unit.CombatProperties.CurrentHP + amount).Clamp(0, unit.Stats[CombatStats.HP]);
			return unit.CombatProperties.CurrentHP - initial;
		}

		public int ChangeMP(IUnit unit, int amount)
		{
			int initial = unit.CombatProperties.CurrentMP;
			unit.CombatProperties.CurrentMP = (unit.CombatProperties.CurrentMP + amount).Clamp(0, unit.Stats[CombatStats.MP]);
			return unit.CombatProperties.CurrentMP - initial;
		}

		public Damage CalculateAttackDamage(IUnit unit, bool crit = false)
		{
			IWeapon weapon = unit.CombatProperties.GetEquippedWeapon();

			return new Damage(
				DamageType.Physical,
				weapon.AttackElement,
				ScaleDamageValue(weapon.BaseDamage + unit.Stats[Attributes.STR], unit.Stats[CombatStats.ATK]) * (crit ? weapon.CritMultiplier : 1),
				unit.Name
			);
		}

		public Damage CalculateSpellDamage(IUnit unit, ISpell spell, bool scale = true, bool crit = false)
		{
			return new Damage(
				spell.DamageType,
				spell.SpellElement,
				ScaleDamageValue(spell.BaseDamage, scale ? unit.Stats[CombatStats.MAG] : 0) * (crit ? 2 : 1),
				unit.Name
			);
		}

		public int CalculateReceivedDamage(IUnit unit, Damage damage)
		{
			int finalDamage;

			if (damage.Type == DamageType.True)
			{
				finalDamage = damage.Value;
			}
			else
			{
				CombatStats resistStat;

				if (damage.Type == DamageType.Magical) resistStat = CombatStats.RES;
				else resistStat = CombatStats.DEF;

				finalDamage = MitigateDamageValue(damage.Value, unit.Stats[resistStat]);
			}

			return finalDamage;
		}

		public HitCheck CheckForHit(IUnit unit, IUnit target)
		{
			double threshold = MathExtensions.Clamp(unit.Stats[CombatStats.HIT] - target.Stats[CombatStats.AVD], 0, 100) / 100f;
			bool hit = new CenterWeightedChecker(threshold).Check();
			double critThreshold = MathExtensions.Clamp(unit.Stats[CombatStats.CRT], 0, 100) / 100f;
			bool crit = hit ? new SuccessChecker(critThreshold).Check() : false;

			return new HitCheck(
				threshold,
				hit,
				critThreshold,
				crit
			);
		}

		public HPLoss ReceiveDamage(IUnit unit, Damage damage)
		{
			int hpLost = -ChangeHP(unit, -CalculateReceivedDamage(unit, damage));

			OnDamageTaken?.Invoke(unit, damage, hpLost);

			return new HPLoss(
				unit.Name,
				hpLost
			);
		}

		public AttackAction Attack(IUnit unit, IUnit target)
		{
			HitCheck hit = CheckForHit(unit, target);
			Damage damage = hit.Hit ? CalculateAttackDamage(unit, hit.Crit) : null;
			HPLoss hp = hit.Hit ? ReceiveDamage(target, damage) : null;
			IList<ILogEntry> effects = hit.Hit ? ApplyEffects(unit.CombatProperties.GetEquippedWeapon(), target, unit, hp.Value) : null;

			return new AttackAction(
				unit,
				target,
				hit,
				damage,
				hp,
				effects
			);
		}

		public SpellAction Cast(IUnit unit, ISpell spell, SpellCastOptions options, params IUnit[] targets)
		{
			int n = targets.Length;
			HitCheck[] hit = new HitCheck[n];
			Damage[] damage = new Damage[n];
			HPLoss[] hpLost = new HPLoss[n];
			IList<ILogEntry>[] effects = new IList<ILogEntry>[n];

			// MP cost (calling layer is responsible for validation)
			unit.CombatProperties.CurrentMP -= options.AdjustedCost > -1 ? options.AdjustedCost : spell.Cost;

			for (int i = 0; i < n; i++)
			{
				// Roll hit for attack type spells
				if (spell.TargetType == SpellTargetType.Attack)
				{
					hit[i] = CheckForHit(unit, targets[i]);
					if (!hit[i].Hit) continue;
				}

				List<ILogEntry> effectsList = new List<ILogEntry>();

				// Damage dealing spells
				if (spell.BaseDamage > 0)
				{
					damage[i] = CalculateSpellDamage(unit, spell, !options.NoScaling, hit[i]?.Crit ?? false);
					hpLost[i] = ReceiveDamage(targets[i], damage[i]);
				}

				// Apply buffs/debuffs
				foreach (IBuff buff in spell.GrantedBuffs)
				{
					ApplyBuff(targets[i], buff, String.Format("{0}'s {1}", unit.Name, spell.Name));
					effectsList.Add(new LogEntry(string.Format("{0} is affected by {1}.", targets[i].Name, buff.Name)));
				}

				// Healing spells
				if (spell.BaseHeal > 0)
				{
					int healValue = ChangeHP(targets[i], spell.BaseHeal * (options.NoScaling ? 1 : unit.Stats[Attributes.WIS]));
					effectsList.Add(new LogEntry(string.Format("{0} is healed for {1} HP.", targets[i].Name, healValue)));
				}

				// Apply other effects
				effectsList.AddRange(ApplyEffects(spell, targets[i], unit, hpLost[i] != null ? hpLost[i].Value : 0));

				effects[i] = effectsList;
			}

			return new SpellAction(
				unit,
				spell,
				targets,
				hit,
				damage,
				hpLost,
				effects
			);
		}

		public SpellAction Cast(IUnit unit, ISpell spell, params IUnit[] targets)
			=> Cast(unit, spell, new SpellCastOptions(), targets);

		public IList<ILogEntry> UseItem(IUnit unit, IUsableItem item, params IUnit[] targets)
		{
			List<ILogEntry> effects = new List<ILogEntry>();
			effects.Add(new LogEntry(string.Format("{0} uses {1}.", unit.Name, item.Name)));
			foreach (IUnit target in targets)
			{
				effects.AddRange(ApplyEffects(item, target, unit));
			}
			return effects;
		}

		public IList<ILogEntry> UseItem(IUnit unit, ISpellItem item, params IUnit[] targets)
		{
			List<ILogEntry> effects = new List<ILogEntry>();
			effects.AddRange(UseItem(unit, (IUsableItem)item, targets));
			effects.Add(Cast(unit, item.Spell, targets));
			return effects;
		}

		public CombatEvaluator(IEffectsRegistry effects)
		{
			this.effects = effects;
		}
	}
}
