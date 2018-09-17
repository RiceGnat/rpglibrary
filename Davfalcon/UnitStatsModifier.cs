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
			statsModifier = new StatsModifier(Target, Additions, Multiplications);
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
	}
}
