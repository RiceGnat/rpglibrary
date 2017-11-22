using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	public interface IUnitEquipmentProperties : IUnitProperties
	{
		IEnumerable<IEquipment> Equipment { get; }
		IDictionary<EquipmentSlot, IEquipment> EquipmentLookup { get; }
		IWeapon EquippedWeapon { get; }
		IEquipment GetEquipment(EquipmentSlot slot);
	}
}
