using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGLibrary;

namespace Davfalcon.Combat
{
	public struct AttackAction : ILogEntry
	{
		public IUnit Attacker;
		public IUnit Defender;
		public Damage DamageDealt;
		public int HPLost;

		public override string ToString()
		{
			return String.Format("{0} attacks {1} with {2}.", Attacker.Name, Defender.Name, Attacker.Properties.GetAs<IUnitCombatProps>().EquippedWeapon.Name) + Environment.NewLine +
				   DamageDealt + Environment.NewLine +
				   String.Format("{0} loses {1} HP.", Defender.Name, HPLost);
		}
	}
}
