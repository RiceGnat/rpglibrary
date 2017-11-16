using System.Collections.Generic;
using RPGLibrary.Items;

namespace Davfalcon
{
	public interface IEquipment : IModifierItem
	{
		EquipmentSlot Slot { get; }
		IList<IBuff> GrantedEffects { get; }
	}
}
