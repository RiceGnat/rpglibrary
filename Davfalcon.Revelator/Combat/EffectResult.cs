using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator.Combat
{
	[Serializable]
	public class EffectResult : ILogEntry
	{
		public string Owner { get; }
		public string Source { get; }
		public string Message { get; }
		public IEnumerable<TargetedUnit> UnitsAffected { get; }

		public EffectResult(IUnit owner, IEffectSource source, IEnumerable<TargetedUnit> unitsAffected)
		{
			Owner = owner.Name;
			Source = source.Name;
			UnitsAffected = unitsAffected.ToNewReadOnlyCollectionSafe();
		}

		public EffectResult(string message)
		{
			Message = message;
		}

		public static EffectResult SetArgsResult(CombatEffectArgs args, IEnumerable<TargetedUnit> unitsAffected)
			=> args.Result = new EffectResult(args.Owner, args.Source, unitsAffected);
	}
}
