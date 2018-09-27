using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator.Engine.Combat
{
	[Serializable]
	public class SpellResult : ILogEntry
	{
		public string Caster{ get; }
		public string Spell{ get; }
		public IEnumerable<ActionResult> Targets { get; }
		public IEnumerable<EffectResult> OtherEffects{ get; }

		public SpellResult(IUnit caster, ISpell spell, IEnumerable<ActionResult> targets, IEnumerable<EffectResult> otherEffects = null)
		{
			Caster = caster.Name;
			Spell = spell.Name;
			Targets = targets.ToNewReadOnlyCollectionSafe();
			OtherEffects = otherEffects.ToNewReadOnlyCollectionSafe();
		}
	}
}
