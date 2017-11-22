using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	public interface IUnitItemManagementProperties : IUnitProperties
	{
		IEnumerable<IEquipment> Equipment { get; }
		IDictionary<EquipmentSlot, IEquipment> EquipmentLookup { get; }
		IWeapon EquippedWeapon { get; }
		IEquipment GetEquipment(EquipmentSlot slot);
		IList<IItem> Inventory { get; }
	}
}
