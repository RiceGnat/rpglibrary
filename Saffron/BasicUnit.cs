using System;
using System.Runtime.Serialization;

namespace Saffron
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
				get { return StatsConstant.Zero; }
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

		public virtual string Name { get; set; }
		public virtual string Class { get; set; }
		public virtual int Level { get; set; }

		public virtual IStats Stats { get { return Modifiers.StatsDetails == StatsDetails ? StatsDetails.Base : StatsDetails.Final; } }
		public virtual IStatsPackage StatsDetails { get { return statsRouter; } }

		public IEditableStats BaseStats { get; protected set; }
		public IUnitModifierStack Modifiers { get; protected set; }

		// Perform initial setup
		protected virtual void Initialize()
		{
			BaseStats = new StatsMap();
			Modifiers = new UnitModifierStack();
		}

		// Set internal object references
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

		public BasicUnit()
		{
			Initialize();
			Link();
		}
	}
}
