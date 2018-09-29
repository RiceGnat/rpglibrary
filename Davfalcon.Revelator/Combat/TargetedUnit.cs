using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator.Combat
{
	[Serializable]
	public class TargetedUnit
	{
		public IUnit Target { get; }
		public HitCheck Hit { get; }
		public Damage DamageDealt { get; }
		public IEnumerable<StatChange> StatChanges { get; }
		public IEnumerable<IBuff> BuffsApplied { get; }
		public IEnumerable<EffectResult> Effects { get; }

		public TargetedUnit(IUnit target, HitCheck hit, Damage damageDealt, IEnumerable<StatChange> statChanges = null, IEnumerable<IBuff> buffsApplied = null, IEnumerable < EffectResult> effects = null)
		{
			Target = target;
			Hit = hit;
			DamageDealt = damageDealt;
			StatChanges = statChanges.ToNewReadOnlyCollectionSafe();
			BuffsApplied = buffsApplied.ToNewReadOnlyCollectionSafe();
			Effects = effects.ToNewReadOnlyCollectionSafe();
		}

		public TargetedUnit(IUnit target, HitCheck hit, Damage damageDealt, StatChange statChange)
			: this(target, hit, damageDealt, new List<StatChange> { statChange })
		{ }
	}
}
