using System.Collections.Generic;

namespace Davfalcon
{
	public interface IEquipment : RPGLibrary.Items.IEquipment
	{
		EquipmentSlot Slot { get; }
		IList<IBuff> GrantedEffects { get; }
	}
}
