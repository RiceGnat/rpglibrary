using System.Collections.Generic;
using Saffron;

namespace Davfalcon
{
	public interface IEquipment : IModifierItem
	{
		EquipmentType SlotType { get; }
		IList<IBuff> GrantedBuffs { get; }
	}
}
