using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Nodes;
using Davfalcon.Randomization;
using Davfalcon.Serialization;

namespace Davfalcon.Revelator.Combat
{
	public class CombatResolver : ICombatResolver, ICombatNodeResolver
	{
		#region Config
		private class Config
		{
			public ICombatOperations Operations { get; set; }
			public CombatStatBinding StatBindings { get; set; }
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
		#endregion

		#region Unit operations
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

		public int AdjustVolatileStat(IUnit unit, Enum stat, int change)
		{
			int initial = unit.VolatileStats[stat];
			unit.VolatileStats[stat] = (unit.VolatileStats[stat] + change).Clamp(0, unit.Stats[stat]);
			return unit.VolatileStats[stat] - initial;
		}

		public void Cleanup(IUnit unit)
		{
			// Clean up volatile stats
			unit.VolatileStats.Clear();

			// Clear all buffs/debuffs
			unit.Buffs.Clear();
		}
		#endregion

		#region Combat operations
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

		public IEnumerable<Enum> GetDamageScalingStats(IEnumerable<Enum> damageTypes)
			=> damageTypes
				.Where(type => config.StatBindings?.GetDamageScalingStat(type) != null)
				.Select(type => config.StatBindings.GetDamageScalingStat(type));

		public Damage CalculateOutgoingDamage(IUnit unit, IDamageSource source, bool scale = true, bool crit = false)
			=> new Damage(
				(scale ? config.Operations.Scale(
					source.BaseDamage + (source.BonusDamageStat != null ? unit.Stats[source.BonusDamageStat] : 0),
					GetDamageScalingStats(source.DamageTypes)
						.Select(stat => unit.Stats[stat])
						.Aggregate(config.Operations.AggregateSeed, config.Operations.Aggregate))
				: source.BaseDamage) * (crit ? source.CritMultiplier : 1),
				unit,
				source.DamageTypes
			);

		public IEnumerable<Enum> GetDamageDefendingStats(IEnumerable<Enum> damageTypes)
			=> damageTypes
				.Where(type => config.StatBindings?.GetDamageResistStat(type) != null)
				.Select(type => config.StatBindings.GetDamageResistStat(type));

		public int CalculateReceivedDamage(IUnit unit, Damage damage)
			=> config.Operations.ScaleInverse(damage.Value, GetDamageDefendingStats(damage.Types)
				.Select(stat => unit.Stats[stat])
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

		public IEnumerable<EffectResult> ApplyEffects(IEffectSource source, IUnit owner, IUnit target, Damage damage = null)
		{
			List<EffectResult> list = new List<EffectResult>();
			foreach (IEffect effect in source.Effects)
			{
				CombatEffectArgs args = new CombatEffectArgs(source, owner, target, this, damage);
				effect.Resolve(args);
				if (args.Result != null) list.Add(args.Result);
			}
			return list;
		}
		#endregion

		#region Nodes
		public INode GetDamageNode(IUnit unit, IDamageSource source)
			=> new DamageNode(source, unit, GetDamageScalingStats(source.DamageTypes), config.Operations);

		public INode GetDefenseNode(IUnit defender, INode damage, IEnumerable<Enum> damageTypes)
			=> new DefenderNode(defender, damage, GetDamageDefendingStats(damageTypes), config.Operations);
		#endregion

		#region Actions
		public IEnumerable<EffectResult> Upkeep(IUnit unit)
		{
			List<EffectResult> effects = new List<EffectResult>();
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

		public ActionResult Attack(IUnit unit, IUnit target, IWeapon weapon)
		{
			HitCheck hit = CheckForHit(unit, target);
			Damage damage = hit.Hit ? CalculateOutgoingDamage(unit, weapon, true, hit.Crit) : Damage.None;
			IEnumerable<StatChange> statChanges = hit.Hit ? ReceiveDamage(target, damage) : null;
			IEnumerable<EffectResult> effects = hit.Hit ? ApplyEffects(weapon, unit, target, damage) : null;

			return new ActionResult(
				unit.DeepClone(),
				weapon.DeepClone(),
				new TargetedUnit(target.DeepClone(), hit, damage, statChanges, null, effects)
			);
		}

		public ActionResult Cast(IUnit unit, ISpell spell, IEnumerable<IUnit> targets, SpellCastOptions options)
		{
			// Get cost resource from options

			List<TargetedUnit> results = new List<TargetedUnit>();
			foreach (IUnit target in targets)
			{
				HitCheck hit;
				Damage damage = Damage.None;
				List<StatChange> statChanges = new List<StatChange>();
				List<IBuff> buffs = new List<IBuff>();
				List<EffectResult> effects = new List<EffectResult>();

				// Roll attack if applicable
				if (options.UseAttack)
				{
					hit = CheckForHit(unit, target);
				}
				else hit = HitCheck.Success;

				if (hit)
				{
					// Deal damage
					if (spell.BaseDamage > 0)
					{
						damage = CalculateOutgoingDamage(unit, spell, options.ScaleDamage, hit.Crit);
						statChanges.AddRange(ReceiveDamage(target, damage));
					}

					foreach (IBuff buff in spell.GrantedBuffs)
					{
						ApplyBuff(target, buff, unit);
						buffs.Add(buff.DeepClone());
					}

					effects.AddRange(ApplyEffects(spell, unit, target, damage));
				}

				results.Add(new TargetedUnit(target, hit, damage, statChanges, buffs, effects));
			}

			return new ActionResult(
				unit.DeepClone(),spell, results);
		}

		public ActionResult Cast(IUnit unit, ISpell spell, params IUnit[] targets)
			=> Cast(unit, spell, targets, new SpellCastOptions());

		public IList<ILogEntry> UseItem(IUnit unit, IUsableItem item, params IUnit[] targets)
		{
			throw new NotImplementedException();
			//List<ILogEntry> effects = new List<ILogEntry>();
			//effects.Add(new LogEntry(string.Format("{0} uses {1}.", unit.Name, item.Name)));
			//foreach (IUnit target in targets)
			//{
			//	effects.AddRange(ApplyEffects(item, unit, target));
			//}
			//return effects;
		}

		public IList<ILogEntry> UseItem(IUnit unit, ISpellItem item, params IUnit[] targets)
		{
			throw new NotImplementedException();
			//List<ILogEntry> effects = new List<ILogEntry>();
			//effects.AddRange(UseItem(unit, (IUsableItem)item, targets));
			//effects.Add(Cast(unit, item.Spell, targets));
			//return effects;
		}
		#endregion

		#region Builder
		private CombatResolver(Config config)
			=> this.config = config;

		public class Builder : IBuilder<ICombatResolver>, IBuilder<ICombatNodeResolver>
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

			public Builder DefineCombatOperations(ICombatOperations operations)
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

			private CombatResolver BuildResolver()
				=> new CombatResolver(config);

			public ICombatResolver Build()
				=> BuildResolver();

			public ICombatNodeResolver BuildNodeResolver()
				=> BuildResolver();

			ICombatNodeResolver IBuilder<ICombatNodeResolver>.Build()
				=> BuildNodeResolver();
		}

		public static ICombatResolver Default { get; } = new Builder().Build();
		#endregion
	}
}
