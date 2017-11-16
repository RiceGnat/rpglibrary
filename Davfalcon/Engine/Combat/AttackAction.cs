using System;
using RPGLibrary;

namespace Davfalcon.Engine.Combat
{
	[Serializable]
	public class AttackAction : ILogEntry
	{
		public readonly string Attacker;
		public readonly string Weapon;
		public readonly string Defender;
		public readonly HitCheck Hit;
		public readonly Damage DamageDealt;
		public readonly HPLoss HPLost;

		public AttackAction(IUnit attacker, IUnit defender, HitCheck hit, Damage damageDealt, HPLoss hpLost)
		{
			Attacker = attacker.Name;
			Weapon = attacker.GetCombatProperties().EquippedWeapon.Name;
			Defender = defender.Name;
			Hit = hit;
			DamageDealt = damageDealt;
			HPLost = hpLost;
		}

		public override string ToString()
		{
			return String.Format("{0} attacks {1} with {2}.", Attacker, Defender, Weapon) + Environment.NewLine +
				   (Hit.Crit ? String.Format("Critical!", Attacker) + Environment.NewLine : "") +
				   (Hit.Hit ? DamageDealt.LogWith(HPLost)
				   : String.Format("{0} misses.", Attacker));
		}
	}
}
