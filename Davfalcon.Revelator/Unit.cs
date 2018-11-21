using System;
using System.Collections.Generic;
using Davfalcon.Builders;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Unit : BasicUnit, IUnit
	{
		[Serializable]
		new protected class Wrapper : BasicUnit.Wrapper, IUnit
		{
			new public IUnit InterfaceUnit => base.InterfaceUnit as IUnit;

			public IDictionary<Enum, int> VolatileStats => InterfaceUnit.VolatileStats;
			public IUnitEquipmentManager<IUnit> Equipment => InterfaceUnit.Equipment;
			public IModifierCollection<IUnit> Buffs => InterfaceUnit.Buffs;
			IModifierCollection<IUnit> IUnit.Modifiers => InterfaceUnit.Modifiers;

			public Wrapper(Davfalcon.IUnit unit) : base(unit) { }
		}

		private ILinkedStatResolver statLinker = new LinkedStatsResolverBase();

		public IDictionary<Enum, int> VolatileStats { get; } = new Dictionary<Enum, int>();
		new public IModifierCollection<IUnit> Modifiers => base.Modifiers as IModifierCollection<IUnit>;
		public IUnitEquipmentManager<IUnit> Equipment { get; protected set; }
		public IModifierCollection<IUnit> Buffs { get; protected set; }

		protected override void Link()
		{
			base.Link();
			statLinker.Bind(this);
		}

		private Unit(IStatsOperations statsOperations, ILinkedStatResolver statLinker)
			: base(new UnitStats(statLinker), statsOperations)
		{
			this.statLinker = statLinker;

			Equipment = new UnitEquipmentManager<IUnit>();
			Buffs = new ModifierCollection<IUnit>();
			Modifiers.Add(Equipment);
			Modifiers.Add(Buffs);
		}

		public static IUnit Build(Func<Builder, IBuilder<IUnit>> builderFunc)
			=> Build(StatsOperations.Default, LinkedStatsResolverBase.Default, builderFunc);

		public static IUnit Build(IStatsOperations statsOperations, ILinkedStatResolver statLinker, Func<Builder, IBuilder<IUnit>> builderFunc)
			=> builderFunc(new Builder(statsOperations, statLinker)).Build();

		public class Builder : BuilderBase<Unit, IUnit, Builder>
		{
			private readonly IStatsOperations statsOperations;
			private readonly ILinkedStatResolver statLinker;

			internal Builder(IStatsOperations statsOperations, ILinkedStatResolver statLinker)
			{
				this.statsOperations = statsOperations;
				this.statLinker = statLinker;
				Reset();
			}

			public override Builder Reset()
			{
				Unit unit = new Unit(statsOperations, statLinker);
				unit.Link();
				return Reset(unit);
			}

			public Builder SetMainDetails(string name, string className = "", int level = 1) => Self(unit =>
			{
				unit.Name = name;
				unit.Class = className;
				unit.Level = level;
			});

			public Builder SetBaseStat(Enum stat, int value) => Self(unit => unit.BaseStats[stat] = value);

			public Builder SetBaseStats(IEnumerable<Enum> stats, int value)
			{
				foreach (Enum stat in stats)
				{
					SetBaseStat(stat, value);
				}
				return Builder;
			}

			public Builder SetAllBaseStats<T>(int value)
			{
				foreach (Enum stat in Enum.GetValues(typeof(T)))
				{
					SetBaseStat(stat, value);
				}
				return Builder;
			}

			public Builder AddEquipmentSlot(Enum slot) => Self(unit => unit.Equipment.AddEquipmentSlot(slot));
			public Builder AddEquipment(IEquipment<IUnit> equipment) => Self(unit => unit.Equipment.Equip(equipment));

			public override IUnit Build()
				=> new Wrapper(base.Build());
		}
	}
}
