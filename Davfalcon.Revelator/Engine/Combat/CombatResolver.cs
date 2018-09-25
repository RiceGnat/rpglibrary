using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Randomization;
using Davfalcon.Serialization;

namespace Davfalcon.Revelator.Engine.Combat
{
	public class CombatResolver : ICombatResolver
	{
		private class Config
		{
			public ICombatOperations Operations { get; set; }
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
			public IList<Enum> VolatileStats { get; } = new List<Enum>();
			public IDictionary<Enum, Enum> DamageScalingMap { get; } = new Dictionary<Enum, Enum>();
			public IDictionary<Enum, Enum> DamageResistMap { get; } = new Dictionary<Enum, Enum>();
			public Enum DefaultDamageResource { get; set; }
			public IDictionary<Enum, IList<Enum>> DamageResourceMap { get; } = new Dictionary<Enum, IList<Enum>>();

			public Enum GetDamageScalingStat(Enum damageType)
				=> DamageScalingMap.ContainsKey(damageType) ? DamageScalingMap[damageType] : null;

			public Enum GetDamageResistStat(Enum damageType)
				=> DamageResistMap.ContainsKey(damageType) ? DamageResistMap[damageType] : null;

			public IEnumerable<Enum> ResolveDamageResource(params Enum[] damageTypes)
			{
				List<Enum> stats = new List<Enum>();
				foreach (Enum type in damageTypes)
				{
					if (DamageResourceMap.ContainsKey(type))
						stats.AddRange(DamageResourceMap[type]);
				}
				stats.Add(DefaultDamageResource);
				return stats;
			}

			public IEnumerable<Enum> ResolveDamageResource(IEnumerable<Enum> damageTypes)
				=> ResolveDamageResource(damageTypes.ToArray());
		}

		private Config config;

		private void AdjustMaxVolatileStats(IUnit unit, IDictionary<Enum, int> prevValues)
		{
			foreach (Enum stat in config.StatBindings.VolatileStats)
			{
				unit.VolatileStats[stat] += unit.Stats[stat] - prevValues[stat];
			}
		}

		public void ApplyBuff(IUnit unit, IBuff buff, IUnit source = null)
		{
			Dictionary<Enum, int> currentValues = new Dictionary<Enum, int>();
			foreach (Enum stat in config.StatBindings.VolatileStats)
			{
				currentValues[stat] = unit.Stats[stat];
			}

			IBuff b = buff.DeepClone();
			b.Source = source ?? unit;
			b.Reset();
			unit.Buffs.Add(b);

			AdjustMaxVolatileStats(unit, currentValues);
		}

		public void RemoveBuff(IUnit unit, IBuff buff)
		{
			Dictionary<Enum, int> currentValues = new Dictionary<Enum, int>();
			foreach (Enum stat in config.StatBindings.VolatileStats)
			{
				currentValues[stat] = unit.Stats[stat];
			}

			unit.Buffs.Remove(buff);

			AdjustMaxVolatileStats(unit, currentValues);
		}

		public void Initialize(IUnit unit)
		{
			// Initialize volatile stats
			foreach (Enum stat in config.StatBindings.VolatileStats)
			{
				unit.VolatileStats[stat] = unit.Stats[stat];
			}

			// Apply buffs granted by equipment
			foreach (IEquipment equip in unit.Equipment.All)
			{
				foreach (IBuff buff in equip.GrantedBuffs)
				{
					ApplyBuff(unit, buff, unit);
				}
			}
		}

		public void Cleanup(IUnit unit)
		{
			// Clean up volatile stats
			unit.VolatileStats.Clear();

			// Clear all buffs/debuffs
			unit.Buffs.Clear();
		}

		public IList<ILogEntry> Upkeep(IUnit unit)
		{
			List<ILogEntry> effects = new List<ILogEntry>();
			List<IBuff> expired = new List<IBuff>();

			foreach (IBuff buff in unit.Buffs)
			{
				// Apply repeating effects
				if (buff.Duration > 0 && buff.Remaining > 0 ||
					buff.Duration == 0)
					effects.AddRange(ApplyEffects(buff, buff.Source, unit));

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

		public HitCheck CheckForHit(IUnit unit, IUnit target)
		{
			int hitChance = config.StatBindings.Hit != null ? unit.Stats[config.StatBindings.Hit] : 100;
			int dodgeChance = config.StatBindings.Dodge != null ? unit.Stats[config.StatBindings.Dodge] : 0;

			double threshold = config.Operations.CalculateHitChance(hitChance, dodgeChance).Clamp(0, 100) / 100f;
			bool hit = new CenterWeightedChecker(threshold).Check();

			if (config.StatBindings.Crit == null)
				return new HitCheck(threshold, hit);

			double critThreshold = MathExtensions.Clamp(unit.Stats[config.StatBindings.Crit], 0, 100) / 100f;
			bool crit = hit ? new SuccessChecker(critThreshold).Check() : false;

			return new HitCheck(threshold, hit, critThreshold, crit);
		}

		public Damage CalculateOutgoingDamage(IUnit unit, IDamageSource source, bool scale = true, bool crit = false)
			=> new Damage(
				(scale ? config.Operations.Scale(
					source.BaseDamage + (source.BonusDamageStat != null ? unit.Stats[source.BonusDamageStat] : 0),
					source.DamageTypes
						.Where(type => config.StatBindings?.GetDamageScalingStat(type) != null)
						.Select(type => unit.Stats[config.StatBindings.GetDamageScalingStat(type)])
						.Aggregate(config.Operations.AggregateSeed, config.Operations.Aggregate))
				: source.BaseDamage) * (crit ? source.CritMultiplier : 1),
				unit,
				source.DamageTypes
			);

		public int CalculateReceivedDamage(IUnit unit, Damage damage)
			=> config.Operations.ScaleInverse(damage.Value, damage.Types
				.Where(type => config.StatBindings?.GetDamageResistStat(type) != null)
				.Select(type => unit.Stats[config.StatBindings.GetDamageResistStat(type)])
				.Aggregate(config.Operations.AggregateSeed, config.Operations.Aggregate));

		public IEnumerable<StatChange> ReceiveDamage(IUnit unit, Damage damage)
		{
			// Calculate the amount of damage the unit will take after resistances
			int adjusted = CalculateReceivedDamage(unit, damage);

			// Get targeted resource points and apply damage pool in order
			List<StatChange> losses = new List<StatChange>();
			foreach (Enum stat in config.StatBindings.ResolveDamageResource(damage.Types))
			{
				// Apply up to the remaining number of points in the stat
				int actual = AdjustVolatileStat(unit, stat, -adjusted);

				// Log the loss
				losses.Add(new StatChange(unit, stat, actual));

				// Subtract from remaining damage pool
				adjusted += actual;

				// Break if all damage is applied
				if (adjusted <= 0)
					break;
			}
			return losses;
		}

		public int AdjustVolatileStat(IUnit unit, Enum stat, int change)
		{
			int initial = unit.VolatileStats[stat];
			unit.VolatileStats[stat] = (unit.VolatileStats[stat] + change).Clamp(0, unit.Stats[stat]);
			return unit.VolatileStats[stat] - initial;
		}

		public IEnumerable<EffectResult> ApplyEffects(IEffectSource source, IUnit owner, IUnit target, Damage damage = null)
		{
			List<EffectResult> list = new List<EffectResult>();
			foreach (IEffect effect in source.Effects)
			{
				CombatEffectArgs args = new CombatEffectArgs(source, owner, target, this, damage);
				effect.Resolve(args);
				list.Add(args.Result);
			}
			return list;
		}

		public AttackResult Attack(IUnit unit, IUnit target, IWeapon weapon)
		{
			HitCheck hit = CheckForHit(unit, target);
			Damage damage = hit.Hit ? CalculateOutgoingDamage(unit, weapon, true, hit.Crit) : Damage.None;
			IEnumerable<StatChange> losses = hit.Hit ? ReceiveDamage(target, damage) : null;
			IEnumerable<ILogEntry> effects = hit.Hit ? ApplyEffects(weapon, unit, target, damage) : null;

			return new AttackResult(
				unit,
				target,
				weapon,
				hit,
				damage,
				losses,
				effects
			);
		}

		public SpellResult Cast(IUnit unit, ISpell spell, SpellCastOptions options, params IUnit[] targets)
		{/*
			int n = targets.Length;
			HitCheck[] hit = new HitCheck[n];
			Damage[] damage = new Damage[n];
			PointLoss[] hpLost = new PointLoss[n];
			IList<ILogEntry>[] effects = new IList<ILogEntry>[n];

			// MP cost (calling layer is responsible for validation)
			unit.CurrentMP -= options.AdjustedCost > -1 ? options.AdjustedCost : spell.Cost;

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
			);*/
			return null;
		}

		public SpellResult Cast(IUnit unit, ISpell spell, params IUnit[] targets)
			=> Cast(unit, spell, new SpellCastOptions(), targets);

		public IList<ILogEntry> UseItem(IUnit unit, IUsableItem item, params IUnit[] targets)
		{
			List<ILogEntry> effects = new List<ILogEntry>();
			effects.Add(new LogEntry(string.Format("{0} uses {1}.", unit.Name, item.Name)));
			foreach (IUnit target in targets)
			{
				effects.AddRange(ApplyEffects(item, unit, target));
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

		private CombatResolver(Config config)
			=> this.config = config;

		public static ICombatResolver Default = new Builder().Build();

		public class Builder
		{
			private Config config;
			private CombatStatBinding statBindings;

			public Builder()
				=> Reset();

			public Builder Reset()
			{
				statBindings = new CombatStatBinding();
				config = new Config
				{
					Operations = CombatOperations.Default,
					StatBindings = statBindings
				};
				return this;
			}

			public Builder SetOperations(ICombatOperations operations)
			{
				config.Operations = operations;
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
				statBindings.VolatileStats.Add(stat);
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

			public Builder AddDamageResourceRule(Enum damageType, Enum resourceStat)
			{
				if (!statBindings.DamageResourceMap.ContainsKey(damageType))
					statBindings.DamageResourceMap[damageType] = new List<Enum>();
				statBindings.DamageResourceMap[damageType].Add(resourceStat);
				return this;
			}

			public Builder SetDefaultDamageResource(Enum resourceStat)
			{
				statBindings.DefaultDamageResource = resourceStat;
				return this;
			}

			public ICombatResolver Build()
				=> new CombatResolver(config);
		}
	}
}
