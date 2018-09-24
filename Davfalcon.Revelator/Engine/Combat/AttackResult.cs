using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator.Engine.Combat
{
	[Serializable]
	public class AttackResult : ActionResult, ILogEntry
	{
		public readonly ActionResult Action;
		public readonly string Weapon;
		public readonly IEnumerable<ILogEntry> OnHitEffects;

		public AttackResult(IUnit attacker, IUnit defender, IWeapon weapon, HitCheck hit, Damage damageDealt, IEnumerable<StatChange> losses, IEnumerable<ILogEntry> effects = null)
			: base(attacker, defender, hit, damageDealt, losses)
		{
			Weapon = weapon.Name;
			OnHitEffects = effects.ToNewReadOnlyCollectionSafe();
		}
	}
}
