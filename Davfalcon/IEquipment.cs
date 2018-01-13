using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	public interface IEquipment : IModifierItem
	{
		EquipmentSlot Slot { get; }
		IList<IBuff> GrantedBuffs { get; }
	}
}
