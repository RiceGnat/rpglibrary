using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator.Engine.Combat
{
	[Serializable]
	public struct AttackAction : ILogEntry
	{
		public readonly string Attacker;
		public readonly string Weapon;
		public readonly string Defender;
		public readonly HitCheck Hit;
		public readonly Damage DamageDealt;
		public readonly IEnumerable<PointLoss> Losses;
		public readonly IEnumerable<ILogEntry> OnHitEffects;

		public AttackAction(IUnit attacker, IUnit defender, IWeapon weapon, HitCheck hit, Damage damageDealt, IEnumerable<PointLoss> losses, IEnumerable<ILogEntry> effects)
		{
			Attacker = attacker.Name;
			Weapon = weapon.Name;
			Defender = defender.Name;
			Hit = hit;
			DamageDealt = damageDealt;
			Losses = losses;
			OnHitEffects = effects == null ? null : new List<ILogEntry>(effects);
		}
	}
}
