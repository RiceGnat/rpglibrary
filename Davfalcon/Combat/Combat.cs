using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGLibrary;
using RPGLibrary.Serialization;

namespace Davfalcon.Combat
{
	public static class Combat
	{
		public IUnitCombatProps GetUnitCombatProps(IUnit unit)
		{
			return unit.Properties.GetAs<IUnitCombatProps>();
		}

		public int ScaleDamageValue(int baseValue, int scaling)
		{
			return (int)(baseValue * (100f + scaling));
		}

		public Damage CalculateAttackDamage(IUnit attacker)
		{
			IWeapon weapon = GetUnitCombatProps(attacker).EquippedWeapon;

			Damage d = new Damage();
			d.Source = attacker.Name;
			d.Value = ScaleDamageValue(weapon.BaseDamage, attacker.Stats[BattleStats.ATK]);
			d.Element = weapon.AttackElement;

			return d;
		}
	}
}
