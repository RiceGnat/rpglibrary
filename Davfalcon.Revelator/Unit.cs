using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Unit : BasicUnit, IUnit
	{
		private ILinkedStatResolver statLinker = new LinkedStatsResolverBase();

		public IDictionary<Enum, int> VolatileStats { get; } = new Dictionary<Enum, int>();
		public IUnitEquipmentManager Equipment { get; protected set; }
		public IUnitModifierStack Buffs { get; protected set; }

		public override void Initialize()
		{
			BaseStats = new UnitStats(statLinker);
			Modifiers = new UnitModifierStack();

			// Internal references will be maintained after deserialization
			Equipment = new UnitEquipmentManager();
			Buffs = new UnitModifierStack();
			Modifiers.Add(Equipment);
			Modifiers.Add(Buffs);

			Link();
		}

		protected override void Link()
		{
			base.Link();
			statLinker.Bind(this);
		}

		private Unit(IStatsResolver statsMath, ILinkedStatResolver statLinker)
			: base(statsMath)
		{
			this.statLinker = statLinker;
		}

		public class Builder : BuilderBase<Unit, IUnit>
		{
			private readonly IStatsResolver statsMath;
			private readonly ILinkedStatResolver statLinker;

			public Builder() :
				this(StatsResolver.Default, LinkedStatsResolverBase.Default)
			{ }

			public Builder(IStatsResolver statsMath, ILinkedStatResolver statLinker)
			{
				this.statsMath = statsMath;
				this.statLinker = statLinker;
				Reset();
			}

			public Builder Reset()
			{
				build = new Unit(statsMath, statLinker);
				build.Initialize();
				return this;
			}

			public Builder SetMainDetails(string name, string className = "", int level = 1)
			{
				build.Name = name;
				build.Class = className;
				build.Level = level;
				return this;
			}

			public Builder SetBaseStat(Enum stat, int value)
			{
				build.BaseStats[stat] = value;
				return this;
			}

			public Builder SetBaseStats(IEnumerable<Enum> stats, int value)
			{
				foreach (Enum stat in stats)
				{
					SetBaseStat(stat, value);
				}
				return this;
			}

			public Builder SetAllBaseStats<T>(int value)
			{
				foreach (Enum stat in Enum.GetValues(typeof(T)))
				{
					SetBaseStat(stat, value);
				}
				return this;
			}
		}
	}
}
