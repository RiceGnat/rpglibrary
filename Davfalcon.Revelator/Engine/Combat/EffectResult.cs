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
		public IEnumerable<EffectResult> ChildEffects { get; }

		public EffectResult(IUnit owner, IEffectSource source, IEnumerable<ActionResult> unitsAffected, IEnumerable<EffectResult> childEffects = null)
		{
			Owner = owner.Name;
			Source = source.Name;
			UnitsAffected = unitsAffected.ToNewReadOnlyCollectionSafe();
			ChildEffects = childEffects.ToNewReadOnlyCollectionSafe();
		}

		public static EffectResult FromArgs(CombatEffectArgs args, IEnumerable<ActionResult> unitsAffected, IEnumerable<EffectResult> childEffects = null)
			=> args.Result = new EffectResult(args.Owner, args.Source, unitsAffected, childEffects);
	}
}
