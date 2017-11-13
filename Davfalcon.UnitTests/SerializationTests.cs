using System;
using System.Collections.Generic;
using Davfalcon.Combat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGLibrary;
using RPGLibrary.Serialization;

namespace Davfalcon.UnitTests
{
	[TestClass]
	public class SerializationTests
	{
		private Unit MakeUnit()
		{
			Unit unit = new Unit
			{
				Name = "Test Unit",
				Class = "Class",
				Level = 1
			};

			foreach (Attributes stat in Enum.GetValues(typeof(Attributes)))
			{
				unit.BaseStats[stat] = 10;
			}

			return unit;
		}

		private Equipment MakeEquipment()
		{
			Equipment armor = new Equipment(EquipmentSlot.Armor);
			armor.Name = "Test Armor";
			armor.Additions[CombatStats.DEF] = 3;
			return armor;
		}

		[TestMethod]
		public void UnitSerialization()
		{
			Unit unit = MakeUnit();
			Unit clone = (Unit)Serializer.DeepClone(unit);

			Assert.AreEqual(unit.Name, clone.Name);
			Assert.AreEqual(unit.Class, clone.Class);
			Assert.AreEqual(unit.Level, clone.Level);
			Assert.AreEqual(unit.Stats[Attributes.STR], clone.Stats[Attributes.STR]);
		}

		[TestMethod]
		public void EquipmentSerialization()
		{
			Equipment armor = MakeEquipment();

			Equipment clone = (Equipment)Serializer.DeepClone(armor);
			Assert.AreEqual(armor.Additions[CombatStats.DEF], clone.Additions[CombatStats.DEF]);
		}

		[TestMethod]
		public void BuffSerialization()
		{
			Buff buff = new Buff();
			buff.Multiplications[CombatStats.DEF] = 10;

			Buff clone = (Buff)Serializer.DeepClone(buff);
			Assert.AreEqual(buff.Multiplications[CombatStats.DEF], clone.Multiplications[CombatStats.DEF]);
		}

		[TestMethod]
		public void EquippedUnitSerialization()
		{
			Unit unit = MakeUnit();

			Equipment armor = MakeEquipment();
			unit.Properties.GetAs<IUnitEquipProps>().Equip(EquipmentSlot.Armor, armor);

			Unit clone = (Unit)Serializer.DeepClone(unit);

			Assert.AreEqual(unit.Stats[CombatStats.DEF], clone.Stats[CombatStats.DEF]);
		}

		private static void BurnDamage(IUnit unit, IBuff buff, IList<ILogEntry> effects)
		{
			unit.GetCombatProps().CurrentHP -= 10;
		}

		[TestMethod]
		public void BuffEventSerialization()
		{
			Unit unit = MakeUnit();

			Buff burn = new Buff();
			burn.Name = "Burn";
			burn.IsDebuff = true;
			burn.UpkeepEffects += BurnDamage;

			unit.ApplyBuff(burn, "none");

			Unit clone = (Unit)Serializer.DeepClone(unit);

			unit.Initialize();
			clone.Initialize();
			unit.Upkeep();
			clone.Upkeep();

			Assert.AreEqual(unit.Modifiers.Count, clone.Modifiers.Count);
			Assert.AreEqual(unit.Stats[CombatStats.HP] - 10, unit.GetCombatProps().CurrentHP);
			Assert.AreEqual(unit.GetCombatProps().CurrentHP, clone.GetCombatProps().CurrentHP);
		}
	}
}
