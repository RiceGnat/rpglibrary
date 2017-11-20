using System;
using Davfalcon.Engine;
using Davfalcon.Engine.Combat;
using Davfalcon.UnitManagement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGLibrary;
using RPGLibrary.Serialization;

namespace Davfalcon.UnitTests
{
	[TestClass]
	public class SerializationTests
	{
		private static Engine.System data;

		[ClassInitialize]
		public static void Load(TestContext context)
		{
			data = Engine.System.Current;
			Engine.System.SetSystem(new Engine.System());
			Engine.System.Current.Effects.LoadTemplate("Burn", (int burnDamage) =>
			{
				return (IUnit unit, string source, IUnit originator) =>
				{
					unit.GetCombatProperties().CurrentHP -= 10;
					return null;
				};
			});
		}

		[ClassCleanup]
		public static void Unload()
		{
			Engine.System.SetSystem(data);
		}

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
			unit.Equip(EquipmentSlot.Armor, armor);

			Unit clone = (Unit)Serializer.DeepClone(unit);

			Assert.AreEqual(unit.Stats[CombatStats.DEF], clone.Stats[CombatStats.DEF]);
		}
		
		[TestMethod]
		public void BuffEventSerialization()
		{
			Unit unit = MakeUnit();

			Buff burn = new Buff();
			burn.Name = "Burn";
			burn.IsDebuff = true;
			burn.UpkeepEffects.Add("Burn", 10);

			unit.ApplyBuff(burn);

			Unit clone = (Unit)Serializer.DeepClone(unit);

			unit.Initialize();
			clone.Initialize();
			unit.Upkeep();
			clone.Upkeep();

			Assert.AreEqual(unit.Modifiers.Count, clone.Modifiers.Count);
			Assert.AreEqual(unit.Stats[CombatStats.HP] - 10, unit.GetCombatProperties().CurrentHP);
			Assert.AreEqual(unit.GetCombatProperties().CurrentHP, clone.GetCombatProperties().CurrentHP);
		}

		[TestMethod]
		public void DeserializedModifierStackReferences()
		{
			Unit unit = MakeUnit();
			Unit clone = (Unit)Serializer.DeepClone(unit);

			Assert.IsNotNull(clone.Buffs.Target);

			Buff buff = new Buff();
			buff.Additions[CombatStats.DEF] = 10;

			clone.ApplyBuff(buff);
			Assert.AreEqual(unit.Stats[CombatStats.DEF] + 10, clone.Stats[CombatStats.DEF]);
		}
	}
}
