using System;
using System.Collections.Generic;
using Davfalcon.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Equipment : UnitStatsModifier, IEquipment
	{
		public Enum SlotType { get; set; }

		public ManagedList<IBuff> GrantedBuffs { get; } = new ManagedList<IBuff>();
		IEnumerable<IBuff> IEquipment.GrantedBuffs => GrantedBuffs.AsReadOnly();

		public class Builder
		{
			protected Equipment equipment;
			protected readonly Enum slot;

			public Builder(Enum slot)
			{
				this.slot = slot;
				Reset();
			}

			public Builder Reset()
			{
				equipment = new Equipment()
				{
					SlotType = slot
				};
				return this;
			}

			public Builder SetName(string name)
			{
				equipment.Name = name;
				return this;
			}

			public Builder SetStatAddition(Enum stat, int value)
			{
				equipment.Additions[stat] = value;
				return this;
			}

			public Builder SetStatMultiplier(Enum stat, int value)
			{
				equipment.Multiplications[stat] = value;
				return this;
			}

			public Builder AddBuff(IBuff buff)
			{
				equipment.GrantedBuffs.Add(buff);
				return this;
			}

			public IEquipment Build()
				=> equipment;
		}
	}
}
