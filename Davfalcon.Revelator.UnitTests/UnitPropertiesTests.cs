using System.Linq;
using Davfalcon.Revelator.Borger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon.Revelator.UnitTests
{
	[TestClass]
	public class UnitPropertiesTests
	{
		[TestMethod]
		public void GetEquipmentSlots()
		{
			IUnit unit = new Unit.Builder().Build();
			unit.ItemProperties.AddEquipmentSlot(EquipmentType.Armor);
			unit.ItemProperties.AddEquipmentSlot(EquipmentType.Weapon);
			unit.ItemProperties.AddEquipmentSlot(EquipmentType.Accessory);
			unit.ItemProperties.AddEquipmentSlot(EquipmentType.Accessory);

			Assert.AreEqual(EquipmentType.Armor, unit.ItemProperties.EquipmentSlots.First());
			Assert.AreEqual(EquipmentType.Weapon, unit.ItemProperties.EquipmentSlots.ElementAt(1));
			Assert.AreEqual(EquipmentType.Accessory, unit.ItemProperties.EquipmentSlots.ElementAt(2));
			Assert.AreEqual(EquipmentType.Accessory, unit.ItemProperties.EquipmentSlots.ElementAt(3));
		}
	}
}
