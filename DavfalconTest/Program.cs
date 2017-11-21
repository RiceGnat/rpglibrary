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
using Davfalcon.UnitManagement;

namespace DavfalconTest
{
	class Program
	{
		const string WEAPON_NAME = "Halberd";
		const string ARMOR_NAME = "Some Armor";
		const string ACCESSORY_NAME = "Shiny Ring";
		const string HP_BAG = "Potato Sack";
		const string SPELL_NAME = "Fireball";
		const string ITEM_NAME = "Wand of Fireball";
		const string BURN_BUFF = "Burn";
		const string HP_BUFF = "Punching Bag";
		const string RESTORE_HP_BUFF = "Restore HP";
		const string RESTORE_HP_EFFECT = "RestoreHP";

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

			unit.Equip(WEAPON_NAME);
			unit.Equip(ARMOR_NAME);
			unit.Equip(ACCESSORY_NAME);

			unit.BaseStats[Attributes.STR]++;
			unit.BaseStats[Attributes.VIT]++;

			PrintUnit(unit);

			enemy.Equip(HP_BAG);

			PrintUnit(enemy);
			Console.WriteLine();

			unit.Initialize();
			enemy.Initialize();

			PrintUnitCombat(enemy);
			Console.WriteLine();

			Console.WriteLine(unit.Attack(enemy));
			Console.WriteLine(enemy.Attack(unit));
			Console.WriteLine(unit.Cast(SystemData.Current.Spells.Get(SPELL_NAME), enemy));
			WriteList(unit.UseItem(SystemData.Current.Items.Get(ITEM_NAME), enemy));
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
			SystemData.Current.Effects.LoadTemplate("Burn", (int burnDamage) =>
			{
				return (IUnit unit, string source, IUnit originator) =>
				{
					Damage d = new Damage(
						DamageType.True,
						Element.Fire,
						burnDamage,
						source);

					return new LogEntry(string.Format("{0} is burned for {1} HP.", unit.Name, unit.ReceiveDamage(d).Value));
				};
			});

			SystemData.Current.Effects.LoadTemplate("RestoreHP", (int unused) =>
			{
				return (IUnit unit, string source, IUnit originator) =>
				{
					int hp = unit.Stats[CombatStats.HP] - unit.GetCombatProperties().CurrentHP;

					unit.GetCombatProperties().CurrentHP += hp;

					return new LogEntry(string.Format("{0} restored {1} HP.", unit.Name, hp));
				};
			});

			Buff heal = new Buff();
			heal.Name = RESTORE_HP_BUFF;
			heal.UpkeepEffects.Add(RESTORE_HP_EFFECT);
			SystemData.Current.Buffs.Load(heal);

			Buff hpbuff = new Buff();
			hpbuff.Name = HP_BUFF;
			hpbuff.Multiplications[CombatStats.HP] = 99999;
			hpbuff.UpkeepEffects.Add(RESTORE_HP_EFFECT);
			SystemData.Current.Buffs.Load(hpbuff);

			Buff burn = new Buff();
			burn.Name = BURN_BUFF;
			burn.Duration = 3;
			burn.IsDebuff = true;
			burn.UpkeepEffects.Add(BURN_BUFF, 10);
			SystemData.Current.Buffs.Load(burn);

			Equipment armor = new Equipment(EquipmentSlot.Armor);
			armor.Name = ARMOR_NAME;
			armor.Additions[CombatStats.DEF] = 3;
			armor.Additions[CombatStats.AVD] = 50;
			SystemData.Current.Equipment.Load(armor);

			Equipment ring = new Equipment(EquipmentSlot.Accessory);
			ring.Name = ACCESSORY_NAME;
			ring.Additions[Attributes.STR] = 1;
			ring.Additions[Attributes.AGI] = 1;
			ring.AddBuff(RESTORE_HP_BUFF);
			SystemData.Current.Equipment.Load(ring);

			Equipment hpbag = new Equipment(EquipmentSlot.Armor);
			hpbag.Name = HP_BAG;
			hpbag.Additions[CombatStats.RES] = 20;
			hpbag.AddBuff(HP_BUFF);
			SystemData.Current.Equipment.Load(hpbag);

			Weapon weapon = new Weapon();
			weapon.Name = WEAPON_NAME;
			weapon.BaseDamage = 50;
			weapon.CritMultiplier = 2;
			weapon.AttackElement = Element.Fire;
			weapon.Type = WeaponType.Axe;
			weapon.Additions[CombatStats.ATK] = 5;
			weapon.Additions[CombatStats.CRT] = 30;
			SystemData.Current.Equipment.Load(weapon);

			Spell spell = new Spell();
			spell.Name = SPELL_NAME;
			spell.SpellElement = Element.Fire;
			spell.DamageType = DamageType.Magical;
			spell.BaseDamage = 60;
			spell.Cost = 30;
			spell.AddBuff(BURN_BUFF);
			SystemData.Current.Spells.Load(spell);

			SpellItem wand = new SpellItem(SystemData.Current.Spells.Get(SPELL_NAME));
			wand.Name = ITEM_NAME;
			SystemData.Current.Items.Load(wand);

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
