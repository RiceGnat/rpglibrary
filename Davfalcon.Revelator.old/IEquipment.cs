using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IEquipment : IItem, IStatsModifier
	{
		EquipmentType SlotType { get; }
		IList<IBuff> GrantedBuffs { get; }
	}
}
