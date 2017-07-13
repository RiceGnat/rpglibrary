using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGLibrary;
using RPGLibrary.Serialization;
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

			Unit enemy = new Unit();
			enemy.Name = "Goblin";

			foreach (Attributes stat in Enum.GetValues(typeof(Attributes)))
			{
				unit.BaseStats[stat] = 10;
				enemy.BaseStats[stat] = 5;
			}

			Weapon weapon = new Weapon();
			weapon.Name = "Halberd";
			weapon.BaseDamage = 50;
			weapon.AttackElement = Element.Fire;
			weapon.Type = WeaponType.Axe;
			weapon.Additions[CombatStats.ATK] = 5;
			
			Equipment armor = new Equipment(EquipmentSlot.Armor);
			armor.Name = "Some Armor";
			armor.Additions[CombatStats.DEF] = 3;

			Equipment ring = new Equipment(EquipmentSlot.Accessory);
			ring.Name = "Shiny Ring";
			ring.Additions[Attributes.STR] = 1;
			ring.Additions[Attributes.AGI] = 1;

			unit.Properties.GetAs<IUnitEquipProps>().Equip(EquipmentSlot.Weapon, weapon);
			unit.Properties.GetAs<IUnitEquipProps>().Equip(EquipmentSlot.Armor, armor);
			unit.Properties.GetAs<IUnitEquipProps>().Equip(EquipmentSlot.Accessory, ring);
			
			unit.BaseStats[Attributes.STR]++;
			unit.BaseStats[Attributes.VIT]++;

			PrintUnit(unit);

			Equipment hpbag = new Equipment(EquipmentSlot.Armor);
			hpbag.Name = "Potato Sack";

			Buff hpbuff = new Buff();
			hpbuff.Name = "HP Bag";
			hpbuff.Multiplications[CombatStats.HP] = 999;

			hpbag.GrantedEffects.Add(hpbuff);

			enemy.Properties.GetAs<IUnitEquipProps>().Equip(EquipmentSlot.Armor, hpbag);

			PrintUnit(enemy);

			Spell spell = new Spell();
			spell.Name = "Fireball";
			spell.SpellElement = Element.Fire;
			spell.DamageType = DamageType.Magical;
			spell.BaseDamage = 60;

			Buff burn = new Buff();
			burn.Name = "Burn";
			burn.Duration = 3;
			burn.IsDebuff = true;

			spell.GrantedBuffs.Add(burn);

			Combat.InitializeUnit(unit);
			Combat.InitializeUnit(enemy);

			Console.WriteLine(unit.Attack(enemy));
			Console.WriteLine(enemy.Attack(unit));
			Console.WriteLine(unit.Cast(spell, enemy));
			foreach (IBuff mod in enemy.Modifiers)
			{
				Console.WriteLine(String.Format("{0} - {1}", mod.Name, mod.Source));
			}

			Console.ReadKey();
		}

		static void PrintSeparator()
		{
			Console.WriteLine(new String('-', 10));
		}

		static void PrintUnit(IUnit unit)
		{
			Console.WriteLine(unit.Name);
			PrintSeparator();
			Console.WriteLine("{0}\t{1}", StatString(unit, CombatStats.HP), StatString(unit, CombatStats.MP));
			Console.WriteLine("{0}\t{1}", StatString(unit, Attributes.STR), StatString(unit, CombatStats.ATK));
			Console.WriteLine("{0}\t{1}", StatString(unit, Attributes.VIT), StatString(unit, CombatStats.DEF));
			Console.WriteLine("{0}\t{1}", StatString(unit, Attributes.AGI), StatString(unit, CombatStats.MAG));
			Console.WriteLine("{0}\t{1}", StatString(unit, Attributes.INT), StatString(unit, CombatStats.RES));
			Console.WriteLine("{0}", StatString(unit, Attributes.WIS));
			PrintSeparator();
			foreach (IEquipment mod in unit.Properties.GetAs<IUnitEquipProps>().Equipment)
			{
				Console.WriteLine(mod.Name);
			}
			PrintSeparator();
			foreach (IBuff mod in unit.Modifiers)
			{
				Console.WriteLine(String.Format("{0} - {1}", mod.Name, mod.Source));
			}
		}

		static string StatString(IUnit unit, Enum stat)
		{
			return String.Format("{0} {1}", stat, unit.Stats[stat]);
		}
	}
}
