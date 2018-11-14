using System;
using System.Collections.Generic;
using Davfalcon.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public abstract class Equipment<T> : UnitStatsModifier<T>, IEquipment<T> where T : IUnit
	{
		public Enum SlotType { get; }

		public ManagedList<IBuff> GrantedBuffs { get; } = new ManagedList<IBuff>();
		IEnumerable<IBuff> IEquipment<T>.GrantedBuffs => GrantedBuffs.AsReadOnly();

		protected Equipment(Enum slot)
		{
			SlotType = slot;
		}
	}
}
