using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator.Engine.Combat
{
	[Serializable]
	public class ActionResult : ILogEntry
	{
		public string Unit { get; }
		public string Target { get; }
		public HitCheck Hit { get; }
		public Damage DamageDealt { get; }
		public IEnumerable<StatChange> StatChanges { get; }

		public ActionResult(IUnit unit, IUnit target, HitCheck hit, Damage damageDealt, IEnumerable<StatChange> statChanges = null)
		{
			Unit = unit.Name;
			Target = target.Name;
			Hit = hit;
			DamageDealt = damageDealt;
			StatChanges = statChanges.ToNewReadOnlyCollectionSafe();
		}

		public ActionResult(IUnit unit, IUnit target, HitCheck hit, Damage damageDealt, StatChange statChange)
			: this(unit, target, hit, damageDealt, new List<StatChange> { statChange })
		{ }
	}
}
