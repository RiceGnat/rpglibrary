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

		protected override IStatsPackage GetStatsResolver()
			=> GetStatsResolver<IEquipment>(this);

		protected Equipment(Enum slot, IStatsOperations operations)
			: base(operations)
		{
			SlotType = slot;
		}

		public class Builder : BuilderBase<Equipment, IEquipment>
		{
			private readonly Enum slot;
			private readonly IStatsOperations operations;

			public Builder(Enum slot)
				: this(slot, StatsOperations.Default)
			{ }

			public Builder(Enum slot, IStatsOperations operations)
			{
				this.slot = slot;
				this.operations = operations;
				Reset();
			}

			public Builder Reset()
			{
				build = new Equipment(slot, operations);
				return this;
			}

			public Builder SetName(string name)
			{
				build.Name = name;
				return this;
			}

			public Builder SetStatAddition(Enum stat, int value)
			{
				build.Additions[stat] = value;
				return this;
			}

			public Builder SetStatMultiplier(Enum stat, int value)
			{
				build.Multipliers[stat] = value;
				return this;
			}

			public Builder AddBuff(IBuff buff)
			{
				build.GrantedBuffs.Add(buff);
				return this;
			}
		}
	}
}
