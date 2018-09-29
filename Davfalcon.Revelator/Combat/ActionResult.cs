using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator.Combat
{
	[Serializable]
	public class ActionResult
	{
		public IUnit Unit { get; }
		public IWeapon Weapon { get; }
		public ISpell Spell { get; }
		public IEnumerable<TargetedUnit> Targets { get; }

		public ActionResult(IUnit unit, IWeapon weapon, ISpell spell, IEnumerable<TargetedUnit> targets)
		{
			Unit = unit;
			Weapon = weapon;
			Spell = spell;
			Targets = targets.ToNewReadOnlyCollectionSafe();
		}

		public ActionResult(IUnit unit, IWeapon weapon, ISpell spell, params TargetedUnit[] targets)
			: this(unit, weapon, spell, targets as IEnumerable<TargetedUnit>)
		{ }

		public ActionResult(IUnit attacker, IWeapon weapon, TargetedUnit target)
			: this(attacker, weapon, null, target)
		{ }

		public ActionResult(IUnit caster, ISpell spell, IEnumerable<TargetedUnit> targets)
			: this(caster, null, spell, targets)
		{ }
	}
}
