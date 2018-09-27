using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator.Engine.Combat
{
	[Serializable]
	public class AttackResult : ActionResult, ILogEntry
	{
		public ActionResult Action { get; }
		public string Weapon { get; }
		public IEnumerable<EffectResult> OnHitEffects { get; }

		public AttackResult(IUnit attacker, IUnit defender, IWeapon weapon, HitCheck hit, Damage damageDealt, IEnumerable<StatChange> losses, IEnumerable<EffectResult> effects = null)
			: base(attacker, defender, hit, damageDealt, losses)
		{
			Weapon = weapon.Name;
			OnHitEffects = effects.ToNewReadOnlyCollectionSafe();
		}
	}
}
