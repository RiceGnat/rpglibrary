using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Randomization;
using Davfalcon.Serialization;

namespace Davfalcon.Revelator.Engine.Combat
{
	public class CombatEvaluator : ICombatEvaluator
	{
		private Config config;

		public event BuffEventHandler OnBuffApplied;
		public event DamageEventHandler OnDamageTaken;

		private IList<ILogEntry> ApplyEffects(IEffectSource source, IUnit target, IUnit originator)
			=> config.Effects.ApplyEffects(source, target, originator);

		private IList<ILogEntry> ApplyEffects(IEffectSource source, IUnit target, IUnit originator, int value)
			=> config.Effects.ApplyEffects(source, target, originator, value);

		private void AdjustVolatileStats(IUnit unit, IDictionary<Enum, int> prevValues)
		{
			foreach (Enum stat in config.StatBindings.VolatileStats)
			{
				unit.CombatProperties.VolatileStats[stat] += unit.Stats[stat] - prevValues[stat];
			}
		}

		public void ApplyBuff(IUnit unit, IBuff buff, string source = null)
		{
			Dictionary<Enum, int> currentValues = new Dictionary<Enum, int>();
			foreach (Enum stat in config.StatBindings.VolatileStats)
			{
				currentValues[stat] = unit.Stats[stat];
			}
			
			IBuff b = (IBuff)Serializer.DeepClone(buff);
			b.Source = source;
			b.Reset();
			unit.CombatProperties.Buffs.Add(b);

			AdjustVolatileStats(unit, currentValues);

			OnBuffApplied?.Invoke(unit, buff);
		}

		public void RemoveBuff(IUnit unit, IBuff buff)
		{
			Dictionary<Enum, int> currentValues = new Dictionary<Enum, int>();
			foreach (Enum stat in config.StatBindings.VolatileStats)
			{
				currentValues[stat] = unit.Stats[stat];
			}

			unit.CombatProperties.Buffs.Remove(buff);

			AdjustVolatileStats(unit, currentValues);
		}

		public void Initialize(IUnit unit)
		{
			// Initialize volatile stats
			foreach (Enum stat in config.StatBindings.VolatileStats)
			{
				unit.CombatProperties.VolatileStats[stat] = unit.Stats[stat];
			}

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
			// Clean up volatile stats
			unit.CombatProperties.VolatileStats.Clear();

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

		public int ChangeHP(IUnit unit, int amount)
		{
			int initial = unit.CombatProperties.CurrentHP;
			unit.CombatProperties.CurrentHP = (unit.CombatProperties.CurrentHP + amount).Clamp(0, unit.Stats[config.HPStat]);
			return unit.CombatProperties.CurrentHP - initial;
		}

		public int ChangeMP(IUnit unit, int amount)
		{
			int initial = unit.CombatProperties.CurrentMP;
			unit.CombatProperties.CurrentMP = (unit.CombatProperties.CurrentMP + amount).Clamp(0, unit.Stats[config.MPStat]);
			return unit.CombatProperties.CurrentMP - initial;
		}

		public Damage CalculateOutgoingDamage(IUnit unit, IDamageSource source, bool scale = true, bool crit = false)
			=> new Damage(
				(scale ? config.Math.Scale(
					source.BaseDamage + (source.BonusDamageStat != null ? unit.Stats[source.BonusDamageStat] : 0),
					source.DamageTypes
						.Where(type => config.StatBindings?.GetDamageScalingStat(type) != null)
						.Select(type => unit.Stats[config.StatBindings.GetDamageScalingStat(type)])
						.Aggregate(config.Math.AggregateSeed, config.Math.Aggregate))
				: source.BaseDamage) * (crit ? source.CritMultiplier : 1),
				unit.Name,
				source.DamageTypes
			);

		public int CalculateReceivedDamage(IUnit unit, Damage damage)
			=> config.Math.ScaleInverse(damage.Value, damage.Types
				.Where(type => config.StatBindings?.GetDamageResistStat(type) != null)
				.Select(type => unit.Stats[config.StatBindings.GetDamageResistStat(type)])
				.Aggregate(config.Math.AggregateSeed, config.Math.Aggregate));


		public HitCheck CheckForHit(IUnit unit, IUnit target)
		{
			int hitChance = config.StatBindings.Hit != null ? unit.Stats[config.StatBindings.Hit] : 100;
			int dodgeChance = config.StatBindings.Dodge!= null ? unit.Stats[config.StatBindings.Dodge] : 0;

			double threshold = config.Math.CalculateHitChance(hitChance, dodgeChance).Clamp(0, 100) / 100f;
			bool hit = new CenterWeightedChecker(threshold).Check();

			if (config.StatBindings.Crit == null)
				return new HitCheck(threshold, hit);

			double critThreshold = MathExtensions.Clamp(unit.Stats[config.StatBindings.Crit], 0, 100) / 100f;
			bool crit = hit ? new SuccessChecker(critThreshold).Check() : false;

			return new HitCheck(threshold, hit, critThreshold, crit);
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

		public AttackAction Attack(IUnit unit, IUnit target, IWeapon weapon)
		{
			HitCheck hit = CheckForHit(unit, target);
			Damage damage = hit.Hit ? CalculateOutgoingDamage(unit, weapon, hit.Crit) : null;
			HPLoss hp = hit.Hit ? ReceiveDamage(target, damage) : null;
			IList<ILogEntry> effects = hit.Hit ? ApplyEffects(weapon, target, unit, hp.Value) : null;

			return new AttackAction(
				unit,
				target,
				weapon,
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
				if (config.SpellAttackType != null && spell.TargetType == config.SpellAttackType)
				{
					hit[i] = CheckForHit(unit, targets[i]);
					if (!hit[i].Hit) continue;
				}
				else
				{
					hit[i] = HitCheck.Success;
				}

				List<ILogEntry> effectsList = new List<ILogEntry>();

				// Damage dealing spells
				if (spell.BaseDamage > 0)
				{
					damage[i] = CalculateOutgoingDamage(unit, spell, !options.NoScaling, hit[i].Crit);
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
					// Need to genericize scaling for heals
					int healValue = ChangeHP(targets[i], spell.BaseHeal);
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

		private CombatEvaluator(Config config)
		{
			this.config = config;
		}

		public static ICombatEvaluator Default = new Builder().Build();

		private class Config
		{
			public IEffectsRegistry Effects { get; set; }
			public ICombatMath Math { get; set; }
			public CombatStatBinding StatBindings { get; set; }
			public Enum SpellAttackType { get; set; }
			public Enum HPStat { get; set; }
			public Enum MPStat { get; set; }
		}

		private class CombatStatBinding
		{
			public Enum Hit { get; set; }
			public Enum Dodge { get; set; }
			public Enum Crit { get; set; }
			public List<Enum> VolatileStatsEditable { get; } = new List<Enum>();
			public IEnumerable<Enum> VolatileStats => VolatileStatsEditable.AsReadOnly();
			public IDictionary<Enum, Enum> DamageScalingMap { get; } = new Dictionary<Enum, Enum>();
			public IDictionary<Enum, Enum> DamageResistMap { get; } = new Dictionary<Enum, Enum>();

			public Enum GetDamageScalingStat(Enum damageType)
				=> DamageScalingMap.ContainsKey(damageType) ? DamageScalingMap[damageType] : null;

			public Enum GetDamageResistStat(Enum damageType)
				=> DamageResistMap.ContainsKey(damageType) ? DamageResistMap[damageType] : null;
		}

		public class Builder
		{
			private Config config;
			private CombatStatBinding statBindings;

			public Builder Initialize()
			{
				statBindings = new CombatStatBinding();
				config = new Config
				{
					Math = CombatMath.Default,
					StatBindings = statBindings
				};
				return this;
			}

			public Builder SetEffects(IEffectsRegistry effectsRegistry)
			{
				config.Effects = effectsRegistry;
				return this;
			}

			public Builder SetMath(ICombatMath combatMath)
			{
				config.Math = combatMath;
				return this;
			}

			public Builder SetHitStats(Enum hit, Enum dodge = null, Enum crit = null)
			{
				statBindings.Hit = hit;
				statBindings.Dodge = dodge;
				statBindings.Crit = crit;
				return this;
			}

			public Builder AddVolatileStat(Enum stat)
			{
				statBindings.VolatileStatsEditable.Add(stat);
				return this;
			}

			public Builder AddDamageScaling(Enum damageType, Enum stat)
			{
				statBindings.DamageScalingMap[damageType] = stat;
				return this;
			}

			public Builder AddDamageResist(Enum damageType, Enum stat)
			{
				statBindings.DamageResistMap[damageType] = stat;
				return this;
			}

			public ICombatEvaluator Build()
				=> new CombatEvaluator(config);

			public Builder()
			{
				Initialize();
			}
		}
	}
}
