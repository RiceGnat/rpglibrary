using System;
using System.Collections.Generic;
using Davfalcon.Builders;
using Davfalcon.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public abstract class Equipment<T> : UnitModifierBase<T>, IEquipment<T> where T : IUnit
	{
		public Enum SlotType { get; }

		public ManagedList<IBuff> GrantedBuffs { get; } = new ManagedList<IBuff>();
		IEnumerable<IBuff> IEquipment<T>.GrantedBuffs => GrantedBuffs.AsReadOnly();

		protected Equipment(Enum slot)
		{
			SlotType = slot;
		}

		public abstract class EquipmentBuilderBase<TEquipment, TInterface, TBuilder> : BuilderBase<TEquipment, TInterface, TBuilder>
			where TEquipment : Equipment<T>, TInterface
			where TInterface : IEquipment<T>
			where TBuilder : EquipmentBuilderBase<TEquipment, TInterface, TBuilder>
		{
			protected readonly Enum slot;

			protected EquipmentBuilderBase(Enum slot)
			{
				this.slot = slot;
			}

			public TBuilder SetName(string name) => Self(e => e.Name = name);
			public TBuilder SetStatAddition(Enum stat, int value) => Self(e => e.Additions[stat] = value);
			public TBuilder SetStatMultiplier(Enum stat, int value) => Self(e => e.Multipliers[stat] = value);
			public TBuilder AddBuff(IBuff buff) => Self(e => e.GrantedBuffs.Add(buff));
		}
	}

	[Serializable]
	public class Equipment : Equipment<IUnit>, IEquipment
	{
		protected override IUnit GetAsTargetInterface() => this;

		protected Equipment(Enum slot) : base(slot) { }

		public static IEquipment<IUnit> Build(Enum slot, Func<Builder, IBuilder<IEquipment<IUnit>>> builderFunc)
			=> builderFunc(new Builder(slot)).Build();

		public class Builder : EquipmentBuilderBase<Equipment, IEquipment<IUnit>, Builder>
		{
			internal Builder(Enum slot)
				: base(slot) => Reset();

			public override Builder Reset() => Reset(new Equipment(slot));
		}
	}
}
