using System;
using System.Collections.Generic;
using Davfalcon.Builders;
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

		public abstract class EquipmentBuilder<TEquipment, TInterface, TBuilder> : BuilderBase<TEquipment, TInterface, TBuilder>
			where TEquipment : Equipment, TInterface
			where TInterface : IEquipment
			where TBuilder : EquipmentBuilder<TEquipment, TInterface, TBuilder>
		{
			protected readonly Enum slot;
			protected readonly IStatsOperations operations;
			
			protected EquipmentBuilder(Enum slot, IStatsOperations operations)
			{

				this.slot = slot;
				this.operations = operations;
			}

			public TBuilder SetName(string name)
			{
				build.Name = name;
				return Builder;
			}

			public TBuilder SetStatAddition(Enum stat, int value)
			{
				build.Additions[stat] = value;
				return Builder;
			}

			public TBuilder SetStatMultiplier(Enum stat, int value)
			{
				build.Multipliers[stat] = value;
				return Builder;
			}

			public TBuilder AddBuff(IBuff buff)
			{
				build.GrantedBuffs.Add(buff);
				return Builder;
			}
		}

		public class Builder : EquipmentBuilder<Equipment, IEquipment, Builder>
		{
			public Builder(Enum slot)
				: this(slot, StatsOperations.Default)
			{ }

			public Builder(Enum slot, IStatsOperations operations)
				: base(slot, operations)
			{
				Reset();
			}

			public override Builder Reset()
			{
				build = new Equipment(slot, operations);
				return Builder;
			}
		}
	}
}
