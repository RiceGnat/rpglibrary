using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	public interface IEquipment : IModifierItem
	{
		EquipmentType SlotType { get; }
		IList<IBuff> GrantedBuffs { get; }
	}
}
