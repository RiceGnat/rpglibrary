using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IEquipment : IItem, IStatsModifier
	{
		Enum SlotType { get; }
		IList<IBuff> GrantedBuffs { get; }
	}
}
