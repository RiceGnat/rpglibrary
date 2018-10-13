using System;
using System.Collections.Generic;
using Davfalcon.Builders;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Unit : BasicUnit, IUnit
	{
		private ILinkedStatResolver statLinker = new LinkedStatsResolverBase();

		public IDictionary<Enum, int> VolatileStats { get; } = new Dictionary<Enum, int>();
		public IUnitEquipmentManager Equipment { get; protected set; }
		public IUnitModifierStack Buffs { get; protected set; }

		protected override void Link()
		{
			base.Link();
			statLinker.Bind(this);
		}

		private Unit(IStatsOperations statsOperations, ILinkedStatResolver statLinker)
			: base(new UnitStats(statLinker), statsOperations)
		{
			this.statLinker = statLinker;

			Equipment = new UnitEquipmentManager();
			Buffs = new UnitModifierStack();
			Modifiers.Add(Equipment);
			Modifiers.Add(Buffs);
		}

		public class Builder : BuilderBase<Unit, IUnit, Builder>
		{
			private readonly IStatsOperations statsOperations;
			private readonly ILinkedStatResolver statLinker;

			public Builder() :
				this(StatsOperations.Default, LinkedStatsResolverBase.Default)
			{ }

			public Builder(IStatsOperations statsOperations, ILinkedStatResolver statLinker)
			{
				this.statsOperations = statsOperations;
				this.statLinker = statLinker;
				Reset();
			}

			public override Builder Reset()
			{
				build = new Unit(statsOperations, statLinker);
				build.Link();
				return Builder;
			}

			public Builder SetMainDetails(string name, string className = "", int level = 1)
			{
				build.Name = name;
				build.Class = className;
				build.Level = level;
				return Builder;
			}

			public Builder SetBaseStat(Enum stat, int value)
			{
				build.BaseStats[stat] = value;
				return Builder;
			}

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
		}
	}
}
