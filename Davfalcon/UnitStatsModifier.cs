using System;

namespace Davfalcon
{
	/// <summary>
	/// Modify a unit's stats.
	/// </summary>
	[Serializable]
	public class UnitStatsModifier : UnitModifier, IEditableStatsModifier, IStatsModifier, IUnit
	{
		private class StatsModifier : IStatsPackage
		{
			private IUnit unit;
			private StatsAggregator additions;
			private StatsAggregator multiplications;
			private StatsCalculator final;

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

			public StatsModifier(IUnit unit, IStats additions, IStats multiplications, IStatsResolver calculator)
			{
				this.unit = unit;
				
				// Always use default aggregator for addition (sum)
				this.additions = new StatsAggregator(unit.StatsDetails.Additions, additions);
				this.multiplications = new StatsAggregator(unit.StatsDetails.Multiplications, multiplications, calculator);

				// The final calculation will use the defined formula (or default if not specified)
				final = new StatsCalculator(Base, Additions, Multiplications, calculator);
			}
		}

		[NonSerialized]
		private StatsModifier statsModifier;

		private readonly IStatsResolver calculator;

		/// <summary>
		/// Gets or sets the values to be added to each stat.
		/// </summary>
		public IEditableStats Additions { get; set; }

		/// <summary>
		/// Gets or sets the values to be multiplied with each stat.
		/// </summary>
		public IEditableStats Multiplications { get; set; }

		/// <summary>
		/// Binds the modifier to an <see cref="IUnit"/>.
		/// </summary>
		/// <param name="target">The <see cref="IUnit"/> to bind the modifier to.</param>
		public override void Bind(IUnit target)
		{
			base.Bind(target);
			statsModifier = new StatsModifier(Target, Additions, Multiplications, calculator);
		}

		IStats IUnit.Stats => statsModifier.Final;
		IStatsPackage IUnit.StatsDetails => statsModifier;

		IStats IStatsModifier.Additions => Additions;
		IStats IStatsModifier.Multiplications => Multiplications;

		/// <summary>
		/// Initializes a new <see cref="UnitStatsModifier"/> with no values set.
		/// </summary>
		public UnitStatsModifier()
		{
			Additions = new StatsMap();
			Multiplications = new StatsMap();
		}

		public UnitStatsModifier(IStatsResolver calculator)
			: this()
		{
			this.calculator = calculator;
		}
	}
}
