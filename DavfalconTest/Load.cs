using Davfalcon.Engine;
using Davfalcon.Engine.Combat;
using Davfalcon.Engine.Setup;
using RPGLibrary;
using RPGLibrary.Randomization;

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
			SampleLibrary.LoadEffects();
		}

		private static void LoadBuffs()
		{
			Buff heal = new Buff();
			heal.Name = RESTORE_HP_BUFF;
			heal.UpkeepEffects.Add("Heal", 100);
			SystemData.Current.Buffs.Load(heal);

			Buff hpbuff = new Buff();
			hpbuff.Name = HP_BUFF;
			hpbuff.Multiplications[CombatStats.HP] = 99999;
			hpbuff.UpkeepEffects.Add("Heal", 100);
			SystemData.Current.Buffs.Load(hpbuff);

			Buff burn = new Buff
			{
				Name = BURN_BUFF,
				Duration = 3,
				IsDebuff = true
			};
			burn.UpkeepEffects.Add("Burn", 10);
			SystemData.Current.Buffs.Load(burn);

			Buff scorch = new Buff
			{
				Name = "Scorched",
				Duration = 5,
				IsDebuff = true
			};
			scorch.UpkeepEffects.Add("Burn", 20);
			SystemData.Current.Buffs.Load(scorch);
		}

		private static void LoadSpells()
		{
			Spell spell = new Spell();
			spell.Name = SPELL_NAME;
			spell.SpellElement = Element.Fire;
			spell.DamageType = DamageType.Magical;
			spell.BaseDamage = 60;
			spell.Cost = 30;
			spell.CastEffects.Add("DebuffChance", 75, BURN_BUFF);
			SystemData.Current.Spells.Load(spell);

			spell = new Spell();
			spell.Name = "Scorching Ray";
			spell.SpellElement = Element.Fire;
			spell.DamageType = DamageType.Magical;
			spell.TargetType = SpellTargetType.Attack;
			spell.BaseDamage = 80;
			spell.Cost = 30;
			spell.AddBuff("Scorched");
			spell.CastEffects.Add("Lifelink", 100);
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
			SystemData.Current.Weapons.Load(weapon);

			SpellItem wand = new SpellItem(SystemData.Current.Spells.Get(SPELL_NAME));
			wand.Name = ITEM_NAME;
			SystemData.Current.Items.Load(wand);
		}
	}
}
