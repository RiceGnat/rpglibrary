using System;
using System.Collections.Generic;
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
		public readonly IList<ILogEntry> OnHitEffects;

		public AttackAction(IUnit attacker, IUnit defender, HitCheck hit, Damage damageDealt, HPLoss hpLost, IList<ILogEntry> effects)
		{
			Attacker = attacker.Name;
			Weapon = attacker.GetCombatProperties().EquippedWeapon.Name;
			Defender = defender.Name;
			Hit = hit;
			DamageDealt = damageDealt;
			HPLost = hpLost;
			OnHitEffects = effects == null ? null : new List<ILogEntry>(effects);
		}

		public override string ToString()
		{
			String s = String.Format("{0} attacks {1} with {2}.", Attacker, Defender, Weapon) + Environment.NewLine +
				   (Hit.Crit ? String.Format("Critical!", Attacker) + Environment.NewLine : "") +
				   (Hit.Hit ? DamageDealt.LogWith(HPLost)
				   : String.Format("{0} misses.", Attacker));

			if (OnHitEffects != null)
				foreach (ILogEntry effect in OnHitEffects)
					s += Environment.NewLine + effect;

			return s;
		}
	}
}
