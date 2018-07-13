using System.Collections.Generic;
using Saffron;

namespace Davfalcon
{
	public interface IEquipment : IItem, IStatsModifier
	{
		EquipmentType SlotType { get; }
		IList<IBuff> GrantedBuffs { get; }
	}
}
