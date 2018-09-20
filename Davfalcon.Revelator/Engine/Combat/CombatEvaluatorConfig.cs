using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator.Engine.Combat
{
	public struct CombatEvaluatorConfig
	{
		public IEffectsRegistry Effects { get; private set; }
		public ICombatStatBinding StatBindings { get; private set; }
		public ICombatMath Math { get; private set; }
		public Enum SpellAttackType { get; private set; }
		public Enum HPStat { get; private set; }
		public Enum MPStat { get; private set; }

		public static CombatEvaluatorConfig Default = new Builder().Build();

		private class CombatStatBinding : ICombatStatBinding
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
			private CombatEvaluatorConfig config;
			private CombatStatBinding statBindings;

			public Builder Initialize()
			{
				statBindings = new CombatStatBinding();
				config = new CombatEvaluatorConfig
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

			public CombatEvaluatorConfig Build()
				=> config;

			public Builder()
			{
				Initialize();
			}
		}
	}
}
