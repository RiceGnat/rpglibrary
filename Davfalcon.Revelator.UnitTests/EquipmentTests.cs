using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Davfalcon.Revelator.Borger;
using Davfalcon.Serialization;

namespace Davfalcon.Revelator.UnitTests
{
	[TestClass]
	public class EquipmentTests
	{
		private const Attributes STAT = Attributes.STR;
		private const string UNIT_NAME = "UNIT";
		private const string EQUIP_NAME = "EQUIPMENT";

		private static IUnit MakeUnit()
		{
			IUnit unit = new Unit.Builder()
				.SetMainDetails(UNIT_NAME)
				.SetBaseStats(Enum.GetValues(typeof(Attributes)), 10)
				.SetBaseStat(STAT, 15)
				.Build();

			unit.Equipment.AddEquipmentSlot(EquipmentType.Armor);
			unit.Equipment.AddEquipmentSlot(EquipmentType.Accessory);
			unit.Equipment.AddEquipmentSlot(EquipmentType.Accessory);

			return unit;
		}

		private static IEquipment MakeEquip(EquipmentType slot, int add, int mult)
			=> new Equipment.Builder(slot)
				.SetName(EQUIP_NAME)
				.SetStatAddition(STAT, add)
				.SetStatMultiplier(STAT, mult)
				.Build();

		[TestMethod]
		public void EquippingSingleEquipment()
		{
			IUnit unit = MakeUnit();
			IEquipment equip = MakeEquip(EquipmentType.Armor, 5, 0);

			unit.Equipment.Equip(equip);

			Assert.AreEqual(20, unit.Stats[STAT]);
			Assert.AreEqual(equip, unit.Equipment.GetEquipment(EquipmentType.Armor));
			Assert.AreEqual(UNIT_NAME, unit.Name);
			Assert.AreEqual(EQUIP_NAME, equip.Name);
		}

		[TestMethod]
		public void StackingMultipleEquipment()
		{
			IUnit unit = MakeUnit();
			IEquipment equip1 = MakeEquip(EquipmentType.Armor, 5, 0);
			IEquipment equip2 = MakeEquip(EquipmentType.Accessory, 10, 0);
			IEquipment equip3 = MakeEquip(EquipmentType.Accessory, 0, 100);

			unit.Equipment.Equip(equip1);
			unit.Equipment.Equip(equip2, 0);
			unit.Equipment.Equip(equip3, 1);

			Assert.AreEqual(60, unit.Stats[STAT]);
			Assert.AreEqual(equip1, unit.Equipment.GetEquipment(EquipmentType.Armor));
			Assert.AreEqual(equip2, unit.Equipment.GetEquipment(EquipmentType.Accessory, 0));
			Assert.AreEqual(equip3, unit.Equipment.GetEquipment(EquipmentType.Accessory, 1));
		}

		[TestMethod]
		public void GetEquipmentNull()
		{
			IUnit unit = MakeUnit();
			Assert.IsNull(unit.Equipment.GetEquipment(EquipmentType.Armor));
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException), "Equipped to a mismatched slot.")]
		public void EquipSlotIndexTypeMismatch()
		{
			IUnit unit = MakeUnit();
			IEquipment equip = MakeEquip(EquipmentType.Accessory, 10, 0);

			unit.Equipment.EquipSlotIndex(equip, 0);
		}

		[TestMethod]
		public void GrantedBuffs()
		{
			IEquipment equip = new Equipment.Builder(EquipmentType.Armor)
				.AddBuff(new Buff.Builder().SetName("test1").Build())
				.AddBuff(new Buff.Builder().SetName("test2").Build())
				.AddBuff(new Buff.Builder().SetName("test3").Build())
				.Build();

			Assert.AreEqual("test1", equip.GrantedBuffs.First().Name);
			Assert.AreEqual("test2", equip.GrantedBuffs.ElementAt(1).Name);
			Assert.AreEqual("test3", equip.GrantedBuffs.ElementAt(2).Name);
		}

		[TestMethod]
		public void Serialization()
		{
			IEquipment equip = MakeEquip(EquipmentType.Accessory, 10, 0);

			IEquipment clone = Serializer.DeepClone(equip);
			Assert.AreEqual(equip.Additions[STAT], clone.Additions[STAT]);
		}

		[TestMethod]
		public void EquippedUnitSerialization()
		{
			IUnit unit = MakeUnit();

			IEquipment equip1 = MakeEquip(EquipmentType.Armor, 5, 0);
			IEquipment equip2 = MakeEquip(EquipmentType.Accessory, 10, 0);

			unit.Equipment.Equip(equip1);
			unit.Equipment.Equip(equip2);

			IUnit clone = Serializer.DeepClone(unit);

			Assert.AreEqual(unit.Stats[STAT], clone.Stats[STAT]);

			IEquipment equip3 = MakeEquip(EquipmentType.Accessory, 0, 100);
			clone.Equipment.Equip(equip3, 1);

			Assert.AreEqual(60, clone.Stats[STAT]);
		}
	}
}
