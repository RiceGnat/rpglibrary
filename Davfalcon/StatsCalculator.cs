using System;

namespace Davfalcon
{
	/// <summary>
	/// Performs math across a set of stats.
	/// </summary>
	public class StatsCalculator : StatsPrototype
	{
		private readonly IStatsResolver calculator;

		private readonly IStats original;
		private readonly IStats additions;
		private readonly IStats multiplications;

		/// <summary>
		/// Gets the resulting stat after performing calculations.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		/// <returns>The stat after calculations.</returns>
		public override int Get(Enum stat)
		{
			return calculator.Calculate(original[stat], additions[stat], multiplications[stat]);
		}

		private StatsCalculator() { }

		/// <summary>
		/// Initializes a new <see cref="StatsCalculator"/> that will calculate using the specified stat operands and calculator.
		/// </summary>
		/// <param name="original">The original set of stats to use.</param>
		/// <param name="additions">A set of values to add to each stat.</param>
		/// <param name="multiplications">A set of values to multiply each stat.</param>
		/// <param name="calculator">An object that specifies the calculation formula to use. If null, the default formula will be used.</param>
		public StatsCalculator(IStats original, IStats additions, IStats multiplications, IStatsResolver calculator)
		{
			this.original = original;
			this.additions = additions;
			this.multiplications = multiplications;

			this.calculator = calculator ?? StatsResolver.Default;
		}

		/// <summary>
		/// Initializes a new <see cref="StatsCalculator"/> that will calculate using the specified stat operands and default calculator.
		/// </summary>
		/// <param name="original">The original set of stats to use.</param>
		/// <param name="additions">A set of values to add to each stat.</param>
		/// <param name="multiplications">A set of values to multiply each stat.</param>
		public StatsCalculator(IStats original, IStats additions, IStats multiplications)
			: this(original, additions, multiplications, null)
		{ }
	}
}
