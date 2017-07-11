using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGLibrary;
using Davfalcon;
using Davfalcon.Combat;

namespace DavfalconTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Unit unit = new Unit();
			unit.Name = "Davfalcon";

			foreach (Attributes stat in Enum.GetValues(typeof(Attributes)))
			{
				unit.BaseStats[stat] = 10;
			}

			PrintUnit(unit);

			Weapon weapon = new Weapon();
			weapon.Name = "Halberd";
			weapon.BaseDamage = 20;
			weapon.AttackElement = Element.Fire;
			weapon.Type = WeaponType.Axe;
			weapon.Additions[BattleStats.ATK] = 5;

			Equipment armor = new Equipment(EquipmentSlot.Armor);
			armor.Additions[BattleStats.DEF] = 3;

			Equipment ring = new Equipment(EquipmentSlot.Accessory);
			ring.Additions[Attributes.STR] = 1;
			ring.Additions[Attributes.VIT] = 1;
			/*
			unit.Properties.GetAs<IUnitEquipProps>().EquipWeapon(weapon);
			unit.Properties.GetAs<IUnitEquipProps>().Equip(EquipmentSlot.Armor, armor);
			unit.Properties.GetAs<IUnitEquipProps>().Equip(EquipmentSlot.Accessory, ring);
			*/

			unit.Modifiers.Add(weapon);

			PrintUnit(unit);

			Console.ReadKey();
		}

		static void PrintUnit(IUnit unit)
		{
			Console.WriteLine(unit.Name);
			Console.WriteLine("{0}\t{1}", StatString(unit, BattleStats.HP), StatString(unit, BattleStats.MP));
			Console.WriteLine("{0}\t{1}", StatString(unit, Attributes.STR), StatString(unit, BattleStats.ATK));
			Console.WriteLine("{0}\t{1}", StatString(unit, Attributes.VIT), StatString(unit, BattleStats.DEF));
			Console.WriteLine("{0}\t{1}", StatString(unit, Attributes.AGI), StatString(unit, BattleStats.MAG));
			Console.WriteLine("{0}\t{1}", StatString(unit, Attributes.INT), StatString(unit, BattleStats.RES));
			Console.WriteLine("{0}", StatString(unit, Attributes.WIS));
		}

		static string StatString(IUnit unit, Enum stat)
		{
			return String.Format("{0} {1}", stat, unit.Stats[stat]);
		}
	}
}
