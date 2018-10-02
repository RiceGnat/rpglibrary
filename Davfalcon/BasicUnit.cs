using System;
using System.Runtime.Serialization;
using Davfalcon.Nodes;
using Davfalcon.Stats;

namespace Davfalcon
{
	/// <summary>
	/// Implements basic unit functionality.
	/// </summary>
	[Serializable]
	public class BasicUnit : IUnit, IEditableName
	{
		private class BaseStatsRouter : IStatsPackage
		{
			private BasicUnit unit;

			public IStats Base => unit.BaseStats;
			public IStats Additions => StatsConstant.Zero;
			public IStats Multipliers => new StatsConstant(unit.aggregator.AggregateSeed);
			public IStats Final => unit.Modifiers.Stats;

			public INode GetBaseStatNode(Enum stat) => StatNode<IUnit>.CopyStatsFrom(unit, unit.BaseStats, stat);
			public IAggregatorNode GetAdditionsNode(Enum stat) => new AggregatorNode($"{stat} additions ({unit.Name})", null);
			public IAggregatorNode GetMultipliersNode(Enum stat) => new AggregatorNode($"{stat} multipliers ({unit.Name})", null, unit.aggregator);
			public INode GetStatNode(Enum stat) => unit.ShortCircuit ? GetBaseStatNode(stat) : unit.Modifiers.StatsDetails.GetStatNode(stat);

			public BaseStatsRouter(BasicUnit unit)
			{
				this.unit = unit;
			}
		}

		[NonSerialized]
		private BaseStatsRouter statsRouter;

		private readonly IAggregator aggregator;

		/// <summary>
		/// Gets or sets the unit's name.
		/// </summary>
		public virtual string Name { get; set; }

		/// <summary>
		/// Gets or sets the unit's class.
		/// </summary>
		public virtual string Class { get; set; }

		/// <summary>
		/// Gets or sets the unit's level.
		/// </summary>
		public virtual int Level { get; set; }

		/// <summary>
		/// Gets a representation of the unit's stats.
		/// </summary>
		public virtual IStats Stats => ShortCircuit ? StatsDetails.Base : StatsDetails.Final;

		/// <summary>
		/// Gets a detailed breakdown of the unit's stats.
		/// </summary>
		public virtual IStatsPackage StatsDetails => statsRouter;

		/// <summary>
		/// Gets an editable version of the unit's base stats.
		/// </summary>
		public IEditableStats BaseStats { get; protected set; }

		/// <summary>
		/// Gets the unit's modifiers.
		/// </summary>
		public IUnitModifierStack Modifiers { get; protected set; }

		protected bool ShortCircuit => Modifiers.StatsDetails == StatsDetails;

		/// <summary>
		/// Perform initial setup.
		/// </summary>
		public virtual void Initialize()
		{
			BaseStats = new StatsMap();
			Modifiers = new UnitModifierStack();
			Link();
		}

		/// <summary>
		/// Set internal object references
		/// </summary>
		protected virtual void Link()
		{
			statsRouter = new BaseStatsRouter(this);

			// This will initiate the modifier rebinding calls
			Modifiers.Bind(this);
		}

		[OnDeserialized]
		private void Rebind(StreamingContext context)
		{
			// Reset object references after deserialization
			Link();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BasicUnit"/> class with no properties set.
		/// </summary>
		public BasicUnit()
			: this(StatsOperations.Default)
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="BasicUnit"/> class with the specified <see cref="IAggregator"/>.
		/// </summary>
		/// <param name="aggregator">Used to define the seed for aggregating stat multipliers.</param>
		public BasicUnit(IAggregator aggregator)
		{
			this.aggregator = aggregator;
		}
	}
}
