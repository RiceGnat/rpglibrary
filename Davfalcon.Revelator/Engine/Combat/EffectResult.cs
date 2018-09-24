using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator.Engine.Combat
{
	[Serializable]
	public class EffectResult : ILogEntry
	{
		public string Owner { get; }
		public string Source { get; }
		public IEnumerable<ActionResult> UnitsAffected { get; }

		public EffectResult(IUnit owner, IEffectSource source, IEnumerable<ActionResult> unitsAffected)
		{
			Owner = owner.Name;
			Source = source.Name;
			UnitsAffected = unitsAffected.ToNewReadOnlyCollectionSafe();
		}

		public static void FromArgs(CombatEffectArgs args, IEnumerable<ActionResult> unitsAffected)
		{
			args.Result = new EffectResult(args.Owner, args.Source, unitsAffected);
		}
	}
}
