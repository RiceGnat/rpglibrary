using System;
using System.Runtime.Serialization;

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

			public IStats Base
			{
				get { return unit.BaseStats; }
			}

			public IStats Additions
			{
				get { return StatsConstant.Zero; }
			}

			public IStats Multiplications
			{
				get { return new StatsConstant(unit.statsMath.AggregateSeed); }
			}

			public IStats Final
			{
				get { return unit.Modifiers.Stats; }
			}

			public BaseStatsRouter(BasicUnit unit)
			{
				this.unit = unit;
			}
		}

		[NonSerialized]
		private BaseStatsRouter statsRouter;

		private readonly IStatsResolver statsMath = StatsResolver.Default;

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
		public virtual IStats Stats { get { return Modifiers.StatsDetails == StatsDetails ? StatsDetails.Base : StatsDetails.Final; } }

		/// <summary>
		/// Gets a detailed breakdown of the unit's stats.
		/// </summary>
		public virtual IStatsPackage StatsDetails { get { return statsRouter; } }

		/// <summary>
		/// Gets an editable version of the unit's base stats.
		/// </summary>
		public IEditableStats BaseStats { get; protected set; }

		/// <summary>
		/// Gets the unit's modifiers.
		/// </summary>
		public IUnitModifierStack Modifiers { get; protected set; }

		/// <summary>
		/// Perform initial setup.
		/// </summary>
		protected virtual void Initialize()
		{
			BaseStats = new StatsMap();
			Modifiers = new UnitModifierStack();
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
		/// Constructs a basic unit with no properties set.
		/// </summary>
		public BasicUnit()
		{
			Initialize();
			Link();
		}

		public BasicUnit(IStatsResolver statsMath)
			: this()
		{
			this.statsMath = statsMath;
		}
	}
}
