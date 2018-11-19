using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IEquipment<T> : IItem, IStatsModifier<T> where T : IUnit
	{
		Enum SlotType { get; }
		IEnumerable<IBuff> GrantedBuffs { get; }
	}

	public interface IEquipment : IEquipment<IUnit> { }
}
