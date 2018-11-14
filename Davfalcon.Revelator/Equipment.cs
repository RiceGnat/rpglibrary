using System;
using System.Collections.Generic;
using Davfalcon.Builders;
using Davfalcon.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Equipment : Equipment<IUnit>
	{
		protected override IUnit InterfaceUnit { get; }

		protected Equipment(Enum slot) : base(slot) {
		}

		public static IEquipment<IUnit> Build(Enum slot, Func<Builder, IBuilder<IEquipment<IUnit>>> builderFunc)
			=> builderFunc(new Builder(slot)).Build();

		public abstract class EquipmentBuilder<TEquipment, TInterface, TBuilder> : BuilderBase<TEquipment, TInterface, TBuilder>
			where TEquipment : Equipment, TInterface
			where TInterface : IEquipment<IUnit>
			where TBuilder : EquipmentBuilder<TEquipment, TInterface, TBuilder>
		{
			protected readonly Enum slot;
			protected readonly IStatsOperations operations;

			protected EquipmentBuilder(Enum slot)
			{
				this.slot = slot;
			}

			public TBuilder SetName(string name) => Self(e => e.Name = name);
			public TBuilder SetStatAddition(Enum stat, int value) => Self(e => e.Additions[stat] = value);
			public TBuilder SetStatMultiplier(Enum stat, int value) => Self(e => e.Multipliers[stat] = value);
			public TBuilder AddBuff(IBuff buff) => Self(e => e.GrantedBuffs.Add(buff));
		}

		public class Builder : EquipmentBuilder<Equipment, IEquipment<IUnit>, Builder>
		{
			internal Builder(Enum slot)
				: base(slot) => Reset();

			public override Builder Reset() => Reset(new Equipment(slot));
		}
	}
}
