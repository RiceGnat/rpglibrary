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

			return unit;
		}

		private static Equipment MakeEquip(EquipmentSlot slot, int add, int mult)
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
			IEquipment equip = MakeEquip(EquipmentSlot.Armor, 5, 0);

			unit.Equip(equip);

			Assert.AreEqual(20, unit.Stats[STAT]);
		}

		[TestMethod]
		public void MultipleEquipment()
		{
			IUnit unit = MakeUnit();
			IEquipment equip1 = MakeEquip(EquipmentSlot.Armor, 5, 0);
			IEquipment equip2 = MakeEquip(EquipmentSlot.Accessory, 0, 100);

			unit.Equip(equip1);
			unit.Equip(equip2);

			Assert.AreEqual(40, unit.Stats[STAT]);
		}
	}
}
