using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Equipment : UnitStatsModifier, IEquipment
	{
		public Enum SlotType { get; set; }

		private readonly List<IBuff> grantedBuffs = new List<IBuff>();
		public IList<IBuff> GrantedBuffs { get { return grantedBuffs; } }
		private readonly IList<IBuff> grantedBuffsReadOnly;
		IList<IBuff> IEquipment.GrantedBuffs { get { return grantedBuffsReadOnly; } }

		protected Equipment(Enum slot)
			: base()
		{
			grantedBuffsReadOnly = grantedBuffs.AsReadOnly();
			SlotType = slot;
		}

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
				equipment = new Equipment(slot);
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
