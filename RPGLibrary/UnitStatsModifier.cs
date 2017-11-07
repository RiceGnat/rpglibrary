using System;

namespace RPGLibrary
{
	/// <summary>
	/// Modify a unit's stats.
	/// </summary>
	[Serializable]
	public class UnitStatsModifier : UnitModifier, IUnit
	{
		private class StatsModifier : IStatsPackage
		{
			private IUnit unit;
			private StatsMath additions;
			private StatsMath multiplications;
			private StatsMath final;

			public IStats Base
			{
				get { return unit.StatsDetails.Base; }
			}

			public IStats Additions
			{
				get { return additions; }
			}

			public IStats Multiplications
			{
				get { return multiplications; }
			}

			public IStats Final
			{
				get { return final; }
			}

			public StatsModifier(IUnit unit, IStats additions, IStats multiplications)
			{
				this.unit = unit;
				this.additions = new StatsMath(unit.StatsDetails.Additions, additions, StatsConstant.Zero);
				this.multiplications = new StatsMath(unit.StatsDetails.Multiplications, multiplications, StatsConstant.Zero);
				final = new StatsMath(Base, Additions, Multiplications);
			}
		}

		[NonSerialized]
		private StatsModifier statsModifier;

		public IStatsEditable Additions { get; set; }
		public IStatsEditable Multiplications { get; set; }

		public override void Bind(IUnit target)
		{
			base.Bind(target);
			statsModifier = new StatsModifier(Target, Additions, Multiplications);
		}

		IStats IUnit.Stats { get { return statsModifier.Final; } }
		IStatsPackage IUnit.StatsDetails { get { return statsModifier; } }

		public UnitStatsModifier()
		{
			Additions = new StatsMap();
			Multiplications = new StatsMap();
		}
	}
}
