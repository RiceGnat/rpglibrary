using System;
using System.Runtime.Serialization;
using Davfalcon.Nodes;

namespace Davfalcon
{
	/// <summary>
	/// Implements basic unit functionality.
	/// </summary>
	[Serializable]
	public class BasicUnit : IUnit, IEditableName
	{
		private class BaseStatsRouter : IStatsDetails
		{
			private BasicUnit unit;

			public IStatsOperations Operations => unit.operations;

			public IStats Base => unit.BaseStats;
			public IStats Additions => StatsConstant.Zero;
			public IStats Multipliers => new StatsConstant(unit.operations.AggregateSeed);
			public IStats Final => Base;

			public INode GetBaseStatNode(Enum stat) => StatNode<IUnit>.From(unit, unit.BaseStats, stat);
			public IAggregatorNode GetAdditionsNode(Enum stat) => new AggregatorNode($"{stat} additions ({unit.Name})", null);
			public IAggregatorNode GetMultipliersNode(Enum stat) => new AggregatorNode($"{stat} multipliers ({unit.Name})", null, unit.operations);
			public INode GetStatNode(Enum stat) => GetBaseStatNode(stat);

			public BaseStatsRouter(BasicUnit unit) => this.unit = unit;
		}

		[Serializable]
		protected class Wrapper : IUnit
		{
			private readonly IUnit unit;

			public IUnit InterfaceUnit => (IUnit)unit.Modifiers;

			public string Name => InterfaceUnit.Name;
			public string Class => InterfaceUnit.Class;
			public int Level => InterfaceUnit.Level;
			public IModifierCollection<IUnit> Modifiers => InterfaceUnit.Modifiers;
			public IStats Stats => InterfaceUnit.Stats;
			public IStatsDetails StatsDetails => InterfaceUnit.StatsDetails;

			public Wrapper(IUnit unit) => this.unit = unit;
		}

		[NonSerialized]
		private BaseStatsRouter statsRouter;

		private readonly IStatsOperations operations;

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
		public virtual IStats Stats => StatsDetails.Final;

		/// <summary>
		/// Gets a detailed breakdown of the unit's stats.
		/// </summary>
		public virtual IStatsDetails StatsDetails => statsRouter;

		/// <summary>
		/// Gets an editable version of the unit's base stats.
		/// </summary>
		public IEditableStats BaseStats { get; protected set; }

		/// <summary>
		/// Gets the unit's modifiers.
		/// </summary>
		public IModifierCollection<IUnit> Modifiers { get; protected set; }

		protected void Initialize()
		{
			Setup();
			Link();
		}

		protected virtual void Setup()
		{
			Modifiers = new UnitModifierCollection<IUnit>();
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

		protected BasicUnit(IEditableStats baseStats, IStatsOperations operations)
		{
			this.operations = operations;
			BaseStats = baseStats;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BasicUnit"/> class with no properties set.
		/// </summary>
		public static IUnit Create(Func<BasicUnit, IUnit> func)
		{
			BasicUnit unit = new BasicUnit(new StatsMap(), StatsOperations.Default);
			unit.Initialize();
			return new Wrapper(func(unit));
		}
	}
}
