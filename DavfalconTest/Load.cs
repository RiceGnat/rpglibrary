﻿using Davfalcon.Engine;
using Davfalcon.Engine.Combat;
using Davfalcon.Engine.UnitManagement;
using RPGLibrary;

namespace Davfalcon
{
	public static class Load
	{
		public const string WEAPON_NAME = "Halberd";
		public const string ARMOR_NAME = "Some Armor";
		public const string ACCESSORY_NAME = "Shiny Ring";
		public const string HP_BAG = "Potato Sack";
		public const string SPELL_NAME = "Fireball";
		public const string ITEM_NAME = "Wand of Fireball";
		public const string BURN_BUFF = "Burn";
		public const string HP_BUFF = "Punching Bag";
		public const string RESTORE_HP_BUFF = "Restore HP";
		public const string RESTORE_HP_EFFECT = "RestoreHP";

		public static void LoadData()
		{
			LoadEffects();
			LoadBuffs();
			LoadSpells();
			LoadItems();

			Damage.LogFullDamage = false;
		}

		private static void LoadEffects()
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
		}

		private static void LoadBuffs()
		{
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
		}

		private static void LoadSpells()
		{
			Spell spell = new Spell();
			spell.Name = SPELL_NAME;
			spell.SpellElement = Element.Fire;
			spell.DamageType = DamageType.Magical;
			spell.BaseDamage = 60;
			spell.Cost = 30;
			spell.AddBuff(BURN_BUFF);
			SystemData.Current.Spells.Load(spell);
		}

		private static void LoadItems()
		{
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

			SpellItem wand = new SpellItem(SystemData.Current.Spells.Get(SPELL_NAME));
			wand.Name = ITEM_NAME;
			SystemData.Current.Items.Load(wand);
		}
	}
}