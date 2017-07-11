using System;
using RPGLibrary;

namespace Davfalcon.Combat
{
	public class AttackAction : ILogEntry
	{
		public readonly string Attacker;
		public readonly string Defender;
		public readonly string Weapon;
		public readonly Damage DamageDealt;
		public readonly HPLoss HPLost;

		public AttackAction(IUnit attacker, IUnit defender, Damage damageDealt, HPLoss hpLost)
		{
			Attacker = attacker.Name;
			Weapon = attacker.GetCombatProps().EquippedWeapon.Name;
			Defender = defender.Name;
			DamageDealt = damageDealt;
			HPLost = hpLost;
		}

		public override string ToString()
		{
			return String.Format("{0} attacks {1} with {2}.", Attacker, Defender, Weapon) + Environment.NewLine +
				   DamageDealt + Environment.NewLine +
				   HPLost;
		}
	}
}
