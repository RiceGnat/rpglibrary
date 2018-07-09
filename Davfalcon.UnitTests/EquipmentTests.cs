using System;
using Davfalcon.Engine.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon.UnitTests
{
	[TestClass]
	public class EquipmentTests
	{
		private const Attributes STAT = Attributes.STR;

		private static Unit MakeUnit()
		{
			Unit unit = new Unit();

			foreach (Attributes stat in Enum.GetValues(typeof(Attributes)))
			{
				unit.BaseStats[stat] = 10;
			}

			unit.BaseStats[STAT] = 15;
			unit.ItemProperties.AddEquipmentSlot(EquipmentType.Armor);
			unit.ItemProperties.AddEquipmentSlot(EquipmentType.Accessory);
			unit.ItemProperties.AddEquipmentSlot(EquipmentType.Accessory);

			return unit;
		}

		private static Equipment MakeEquip(EquipmentType slot, int add, int mult)
		{
			Equipment equipment = new Equipment(slot);

			equipment.Additions[STAT] = add;
			equipment.Multiplications[STAT] = mult;

			return equipment;
		}

		[TestMethod]
		public void SingleEquipment()
		{
			IUnit unit = MakeUnit();
			IEquipment equip = MakeEquip(EquipmentType.Armor, 5, 0);

			unit.ItemProperties.Equip(equip);

			Assert.AreEqual(20, unit.Stats[STAT]);
			Assert.AreEqual(equip, unit.ItemProperties.GetEquipment(EquipmentType.Armor));
		}

		[TestMethod]
		public void MultipleEquipment()
		{
			IUnit unit = MakeUnit();
			IEquipment equip1 = MakeEquip(EquipmentType.Armor, 5, 0);
			IEquipment equip2 = MakeEquip(EquipmentType.Accessory, 10, 0);
			IEquipment equip3 = MakeEquip(EquipmentType.Accessory, 0, 100);

			unit.ItemProperties.Equip(equip1);
			unit.ItemProperties.Equip(equip2, 0);
			unit.ItemProperties.Equip(equip3, 1);

			Assert.AreEqual(60, unit.Stats[STAT]);
			Assert.AreEqual(equip1, unit.ItemProperties.GetEquipment(EquipmentType.Armor));
			Assert.AreEqual(equip2, unit.ItemProperties.GetEquipment(EquipmentType.Accessory, 0));
			Assert.AreEqual(equip3, unit.ItemProperties.GetEquipment(EquipmentType.Accessory, 1));
		}

		[TestMethod]
		public void GetEquipmentNull()
		{
			IUnit unit = MakeUnit();
			Assert.IsNull(unit.ItemProperties.GetEquipment(EquipmentType.Armor));
		}
	}
}
