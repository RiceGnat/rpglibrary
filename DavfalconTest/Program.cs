using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGLibrary;
using RPGLibrary.Serialization;
using Davfalcon.Engine;
using Davfalcon.Engine.Combat;
using Davfalcon.Engine.Management;
using static Davfalcon.Load;

namespace Davfalcon
{
	class Program
	{

		static void Main(string[] args)
		{
			LoadData();

			Unit unit = new Unit();
			unit.Name = "Davfalcon";

			foreach (Attributes stat in Enum.GetValues(typeof(Attributes)))
			{
				unit.BaseStats[stat] = 10;
			}

			unit.Equip(WEAPON_NAME);
			unit.Equip(ARMOR_NAME);
			unit.Equip(ACCESSORY_NAME);

			unit.BaseStats[Attributes.STR]++;
			unit.BaseStats[Attributes.VIT]++;

			Battle battle = new Battle();
			battle.AddUnit(unit, 0);
			battle.AddUnit(GenerateEnemy(), 1);
			battle.AddUnit(GenerateEnemy(), 1);
			battle.AddUnit(GenerateEnemy(), 1);

			Console.WriteLine("Player");
			Console.WriteLine();
			PrintUnit(unit);
			Console.WriteLine("\n");
			Console.WriteLine("Enemies");
			Console.WriteLine();
			foreach (IUnit enemy in battle.GetTeam(1))
			{
				PrintUnit(enemy);
				Console.WriteLine();
			}
			Console.WriteLine();
			Console.ReadKey();

			battle.Start();

			foreach (IUnit enemy in battle.GetTeam(1))
			{
				Console.WriteLine(unit.Attack(enemy));
			}

			Console.WriteLine(unit.Cast(SystemData.Current.Spells.Get(SPELL_NAME), battle.GetTeam(1).ToArray()));
			Console.WriteLine(unit.Cast(SystemData.Current.Spells.Get("Scorching Ray"), battle.GetTeam(1).ToArray()));
			Console.ReadKey();

			while (true)
			{
				Console.Clear();
				WriteList(battle.CurrentUnit.Upkeep());
				Console.WriteLine();

				foreach (IUnit u in battle.GetAllUnits())
				{
					if (battle.GetUnitState(u).Team != battle.CurrentUnitState.Team)
					Console.WriteLine(battle.CurrentUnit.Attack(u));
				}
				Console.WriteLine();

				foreach (IUnit u in battle.TurnOrder)
				{
					PrintUnitCombat(u);
					Console.WriteLine();
				}

				Console.ReadKey();

				battle.NextTurn();
			}

			//unit.Initialize();
			//enemy.Initialize();

			//PrintUnitCombat(enemy);
			//Console.WriteLine();

			//Console.WriteLine(unit.Attack(enemy));
			//Console.WriteLine(enemy.Attack(unit));
			//Console.WriteLine(unit.Cast(SystemData.Current.Spells.Get(SPELL_NAME), enemy));
			//WriteList(unit.UseItem(SystemData.Current.Items.Get(ITEM_NAME), enemy));
			//Console.WriteLine();

			//PrintUnitCombat(enemy);
			//Console.WriteLine();

			//while (true)
			//{
			//	WriteList(unit.Upkeep());
			//	WriteList(enemy.Upkeep());

			//	Console.WriteLine(unit.Attack(enemy));
			//	Console.WriteLine(enemy.Attack(unit));

			//	Console.WriteLine();
			//	PrintUnitCombat(unit);
			//	PrintUnitCombat(enemy);
			//	Console.WriteLine();

			//	Console.ReadKey();
			//}
		}

		static int enemyCount = 0;
		static IUnit GenerateEnemy()
		{
			enemyCount++;

			Unit enemy = new Unit();
			enemy.Name = String.Format("Goblin {0}", enemyCount);

			foreach (Attributes stat in Enum.GetValues(typeof(Attributes)))
			{
				enemy.BaseStats[stat] = 5;
			}

			enemy.Equip(HP_BAG);

			return enemy;
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
