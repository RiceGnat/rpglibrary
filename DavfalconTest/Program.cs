using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGLibrary;
using RPGLibrary.Serialization;
using Davfalcon;
using Davfalcon.Engine;
using Davfalcon.Engine.Combat;
using Davfalcon.Engine.Items;
using Davfalcon.UnitManagement;

namespace DavfalconTest
{
	class Program
	{
		static void Main(string[] args)
		{
			LoadData();

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
			weapon.CritMultiplier = 2;
			weapon.AttackElement = Element.Fire;
			weapon.Type = WeaponType.Axe;
			weapon.Additions[CombatStats.ATK] = 5;
			weapon.Additions[CombatStats.CRT] = 30;

			Equipment armor = new Equipment(EquipmentSlot.Armor);
			armor.Name = "Some Armor";
			armor.Additions[CombatStats.DEF] = 3;
			armor.Additions[CombatStats.AVD] = 50;

			Equipment ring = new Equipment(EquipmentSlot.Accessory);
			ring.Name = "Shiny Ring";
			ring.Additions[Attributes.STR] = 1;
			ring.Additions[Attributes.AGI] = 1;

			Buff healbuff = new Buff();
			healbuff.Name = "Restore HP";
			healbuff.UpkeepEffects.Add("RestoreHP");

			ring.GrantedEffects.Add(healbuff);

			unit.Equip(EquipmentSlot.Weapon, weapon);
			unit.Equip(EquipmentSlot.Armor, armor);
			unit.Equip(EquipmentSlot.Accessory, ring);

			unit.BaseStats[Attributes.STR]++;
			unit.BaseStats[Attributes.VIT]++;

			PrintUnit(unit);

			Equipment hpbag = new Equipment(EquipmentSlot.Armor);
			hpbag.Name = "Potato Sack";
			hpbag.Additions[CombatStats.RES] = 20;

			Buff hpbuff = new Buff();
			hpbuff.Name = "HP Bag";
			hpbuff.Multiplications[CombatStats.HP] = 99999;
			hpbuff.UpkeepEffects.Add("RestoreHP");

			hpbag.GrantedEffects.Add(hpbuff);

			enemy.Equip(EquipmentSlot.Armor, hpbag);

			PrintUnit(enemy);
			Console.WriteLine();

			Spell spell = new Spell();
			spell.Name = "Fireball";
			spell.SpellElement = Element.Fire;
			spell.DamageType = DamageType.Magical;
			spell.BaseDamage = 60;
			spell.Cost = 30;

			Buff burn = new Buff();
			burn.Name = "Burn";
			burn.Duration = 3;
			burn.IsDebuff = true;
			burn.UpkeepEffects.Add("Burn", 10);

			spell.GrantedBuffs.Add(burn);

			SpellItem wand = new SpellItem(spell);
			wand.Name = "Wand of Fireball";

			unit.Initialize();
			enemy.Initialize();

			PrintUnitCombat(enemy);
			Console.WriteLine();

			Console.WriteLine(unit.Attack(enemy));
			Console.WriteLine(enemy.Attack(unit));
			Console.WriteLine(unit.Cast(spell, enemy));
			WriteList(unit.UseItem(wand, enemy));
			Console.WriteLine();

			PrintUnitCombat(enemy);
			Console.WriteLine();

			while (true)
			{
				WriteList(unit.Upkeep());
				WriteList(enemy.Upkeep());

				Console.WriteLine(unit.Attack(enemy));
				Console.WriteLine(enemy.Attack(unit));

				Console.WriteLine();
				PrintUnitCombat(unit);
				PrintUnitCombat(enemy);
				Console.WriteLine();

				Console.ReadKey();
			}
		}

		static void LoadData()
		{
			Data.Current.Effects.LoadTemplate("Burn", (int burnDamage) =>
			{
				return (IUnit unit, string source, IUnit originator) =>
				{
					Damage d = new Damage(
						DamageType.True,
						Element.Fire,
						burnDamage,
						source);

					return new LogEntry(String.Format("{0} is burned for {1} HP.", unit.Name, unit.ReceiveDamage(d).Value));
				};
			});

			Data.Current.Effects.LoadTemplate("RestoreHP", (int unused) =>
			{
				return (IUnit unit, string source, IUnit originator) =>
				{
					int hp = unit.Stats[CombatStats.HP] - unit.GetCombatProperties().CurrentHP;

					unit.GetCombatProperties().CurrentHP += hp;

					return new LogEntry(String.Format("{0} restored {1} HP.", unit.Name, hp));
				};
			});
		}

		static void WriteList(IEnumerable list)
		{
			foreach (object entry in list)
			{
				Console.WriteLine(entry);
			}
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
			Console.WriteLine("{0}\t{1}\t{2}", StatString(unit, Attributes.STR), StatString(unit, CombatStats.ATK), StatString(unit, CombatStats.HIT));
			Console.WriteLine("{0}\t{1}\t{2}", StatString(unit, Attributes.VIT), StatString(unit, CombatStats.DEF), StatString(unit, CombatStats.AVD));
			Console.WriteLine("{0}\t{1}", StatString(unit, Attributes.AGI), StatString(unit, CombatStats.MAG));
			Console.WriteLine("{0}\t{1}", StatString(unit, Attributes.INT), StatString(unit, CombatStats.RES));
			Console.WriteLine("{0}", StatString(unit, Attributes.WIS));
			PrintSeparator();
			foreach (IEquipment mod in unit.GetAllEquipment())
			{
				Console.WriteLine(mod.Name);
			}
		}

		static void PrintUnitCombat(IUnit unit)
		{
			Console.WriteLine(unit.Name);
			Console.WriteLine("HP {0}/{1}\tMP {2}/{3}",
				unit.GetCombatProperties().CurrentHP,
				unit.Stats[CombatStats.HP],
				unit.GetCombatProperties().CurrentMP,
				unit.Stats[CombatStats.MP]);
			PrintSeparator();
			foreach (IBuff mod in unit.GetCombatProperties().Buffs)
			{
				Console.WriteLine("{0} [{1}]{2}", mod.Name, mod.Source, mod.Duration > 0 ? String.Format(" - {0}", mod.Remaining) : String.Empty);
			}
		}

		static string StatString(IUnit unit, Enum stat)
		{
			return String.Format("{0} {1}", stat, unit.Stats[stat]);
		}
	}
}
