using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Unit : BasicUnit, IUnit
	{
		private ILinkedStatResolver statLinker = new LinkedStatsResolverBase();
		private UnitProperties props;

		public IUnitModifierStack Equipment { get; protected set; }
		public IUnitModifierStack Buffs { get; protected set; }

		public IUnitCombatProperties CombatProperties { get => props as IUnitCombatProperties; }
		public IUnitItemProperties ItemProperties { get => props as IUnitItemProperties; }

		public override void Initialize()
		{
			BaseStats = new UnitStats(statLinker);
			Modifiers = new UnitModifierStack();

			// Internal references will be maintained after deserialization
			Equipment = new UnitModifierStack();
			Buffs = new UnitModifierStack();
			Modifiers.Add(Equipment);
			Modifiers.Add(Buffs);

			props = new UnitProperties();

			Link();
		}

		protected override void Link()
		{
			base.Link();
			statLinker.Bind(this);
			props.Bind(this);
		}

		[OnSerializing]
		private void SerializationPrep(StreamingContext context)
		{
			// Equipment will be serialized in properties object
			Equipment.Clear();
		}

		private Unit(IStatsResolver statsMath, ILinkedStatResolver statLinker)
			: base(statsMath)
		{
			this.statLinker = statLinker;
		}

		public class Builder
		{
			private Unit unit;
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
				unit = new Unit(statsMath, statLinker);
				unit.Initialize();
				return this;
			}

			public Builder SetMainDetails(string name, string className = "", int level = 1)
			{
				unit.Name = name;
				unit.Class = className;
				unit.Level = level;
				return this;
			}

			public Builder SetBaseStat(Enum stat, int value)
			{
				unit.BaseStats[stat] = value;
				return this;
			}

			public Builder SetBaseStats(IEnumerable stats, int value)
			{
				foreach (Enum stat in stats)
				{
					unit.BaseStats[stat] = value;
				}
				return this;
			}

			public IUnit Build()
				=> unit;
		}
	}
}
