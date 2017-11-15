using System;
using System.Collections.Generic;
using RPGLibrary;
using RPGLibrary.Randomization;
using RPGLibrary.Serialization;

namespace Davfalcon.Combat
{
	public static class CombatOperations
	{
		public delegate void BuffEventHandler(IUnit unit, IBuff buff);
		public delegate void DamageEventHandler(IUnit unit, Damage damage, int hpLost);

		public static event BuffEventHandler OnBuffApplied;
		public static event DamageEventHandler OnDamageTaken;

		public static IUnitCombatProperties GetCombatProperties(this IUnit unit) => unit.Properties.GetAs<IUnitCombatProperties>();

		public static void ApplyBuff(this IUnit unit, IBuff buff, string source)
		{
			int maxHP = unit.Stats[CombatStats.HP];
			int maxMP = unit.Stats[CombatStats.MP];

			// Copy buff to unit modifiers
			IBuff b = (IBuff)Serializer.DeepClone(buff);
			b.Source = source;
			b.Reset();
			unit.GetCombatProperties().Buffs.Add(b);

			// If unit max HP/MP increased, gain the difference
			unit.GetCombatProperties().CurrentHP += Math.Max(unit.Stats[CombatStats.HP] - maxHP, 0);
			unit.GetCombatProperties().CurrentMP += Math.Max(unit.Stats[CombatStats.MP] - maxMP, 0);

			OnBuffApplied?.Invoke(unit, buff);
		}

		public static void RemoveBuff(this IUnit unit, IBuff buff)
		{
			unit.GetCombatProperties().Buffs.Remove(buff);

			unit.GetCombatProperties().CurrentHP = Math.Min(unit.GetCombatProperties().CurrentHP, unit.Stats[CombatStats.HP]);
			unit.GetCombatProperties().CurrentMP = Math.Min(unit.GetCombatProperties().CurrentMP, unit.Stats[CombatStats.MP]);
		}

		public static void Initialize(this IUnit unit)
		{
			// Set HP/MP to max values
			unit.GetCombatProperties().CurrentHP = unit.Stats[CombatStats.HP];
			unit.GetCombatProperties().CurrentMP = unit.Stats[CombatStats.MP];

			// Apply buffs granted by equipment
			foreach (IEquipment equip in unit.GetCombatProperties().Equipment)
			{
				foreach (IBuff buff in equip.GrantedEffects)
				{
					ApplyBuff(unit, buff, String.Format("{0} ({1})", unit.Name, equip.Name));
				}
			}
		}

		public static void Cleanup(this IUnit unit)
		{
			// Reset HP/MP to 0
			unit.GetCombatProperties().CurrentHP = 0;
			unit.GetCombatProperties().CurrentMP = 0;

			// Clear all buffs/debuffs
			unit.Modifiers.Clear();
		}

		public static IList<ILogEntry> Upkeep(this IUnit unit)
		{
			List<ILogEntry> effects = new List<ILogEntry>();
			List<IBuff> expired = new List<IBuff>();

			foreach (IBuff buff in unit.GetCombatProperties().Buffs)
			{
				// Apply repeating effects
				if (buff.Duration > 0 && buff.Remaining > 0 ||
					buff.Duration == 0)
					effects.AddRange(buff.ApplyUpkeepEffects());

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

		public static int ScaleDamageValue(int baseValue, int scaling)
		{
			return (int)(baseValue * (1 + scaling / 100f));
		}

		public static int MitigateDamageValue(int incomingValue, int resistance)
		{
			return (int)(incomingValue * 100f / (100 + resistance));
		}

		public static Damage CalculateAttackDamage(this IUnit unit)
		{
			IWeapon weapon = unit.GetCombatProperties().EquippedWeapon;

			return new Damage(
				DamageType.Physical,
				weapon.AttackElement,
				ScaleDamageValue(weapon.BaseDamage + unit.Stats[Attributes.STR], unit.Stats[CombatStats.ATK]),
				unit.Name
			);
		}

		public static Damage CalculateSpellDamage(this IUnit unit, ISpell spell)
		{
			return new Damage(
				spell.DamageType,
				spell.SpellElement,
				ScaleDamageValue(spell.BaseDamage, unit.Stats[CombatStats.MAG]),
				unit.Name
			);
		}

		public static int CalculateReceivedDamage(this IUnit unit, Damage damage)
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

		public static HitCheck CheckForHit(this IUnit unit, IUnit target)
		{
			double threshold = MathExtensions.Clamp(unit.Stats[CombatStats.HIT] - target.Stats[CombatStats.AVD], 0, 100) / 100f;
			ISuccessCheck rand = new CenterWeightedChecker(threshold);

			return new HitCheck(
				threshold,
				rand.Check()
			);
		}

		public static HPLoss ReceiveDamage(this IUnit unit, Damage damage)
		{
			int hpLost = unit.CalculateReceivedDamage(damage);

			unit.GetCombatProperties().CurrentHP -= hpLost;

			OnDamageTaken?.Invoke(unit, damage, hpLost);

			return new HPLoss(
				unit.Name,
				hpLost
			);
		}

		// This function may need to be moved to game layer
		public static AttackAction Attack(this IUnit unit, IUnit target)
		{
			Damage damage = unit.CalculateAttackDamage();

			HitCheck hit = unit.CheckForHit(target);

			return new AttackAction(
				unit,
				target,
				hit,
				hit.Success ? damage : null,
				hit.Success ? target.ReceiveDamage(damage) : null
			);
		}

		// This function may need to be moved to game layer
		public static SpellAction Cast(this IUnit unit, ISpell spell, params IUnit[] targets)
		{
			int n = targets.Length;
			HitCheck[] hit = new HitCheck[n];
			Damage[] damage = new Damage[n];
			HPLoss[] hpLost = new HPLoss[n];
			IList<ILogEntry>[] effects = new IList<ILogEntry>[n];

			for (int i = 0; i < n; i++)
			{
				// Roll hit for attack type spells
				if (spell.TargetType == SpellTargetType.Attack)
				{
					hit[i] = unit.CheckForHit(targets[i]);
					if (!hit[i].Success) continue;
				}

				List<ILogEntry> effectsList = new List<ILogEntry>();

				// Damage dealing spells
				if (spell.BaseDamage > 0)
				{
					damage[i] = unit.CalculateSpellDamage(spell);
					hpLost[i] = targets[i].ReceiveDamage(damage[i]);
				}

				// Healing spells
				if (spell.BaseHeal > 0)
				{
					int healValue = spell.BaseHeal * unit.Stats[Attributes.WIS];
					targets[i].GetCombatProperties().CurrentHP += healValue;
					effectsList.Add(new LogEntry(string.Format("{0} is healed for {1} HP.", targets[i].Name, healValue)));
				}

				// Apply buffs/debuffs
				foreach (IBuff buff in spell.GrantedBuffs)
				{
					ApplyBuff(targets[i], buff, String.Format("{0} ({1})", unit.Name, spell.Name));
					effectsList.Add(new LogEntry(string.Format("{0} is affected by {1}.", targets[i].Name, buff.Name)));
				}

				// Apply other effects
				effectsList.AddRange(spell.ApplyCastEffects(unit, targets[i]));

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
	}
}
