namespace Saffron
{
	/// <summary>
	/// Performs math across a set of stats.
	/// </summary>
	public class StatsMath : StatsPrototype, IStatsMath
	{
		private static IStatsMath calculator = new StatsMath();

		/// <summary>
		/// Sets the math operation to use for calculating stat math.
		/// </summary>
		public static IStatsMath Calculator { set { calculator = value; } }

		private IStats original;
		private IStats additions;
		private IStats multiplications;

		/// <summary>
		/// Gets the resulting stat after performing calculations.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		/// <returns>The stat after calculations.</returns>
		public override int Get(string stat)
		{
			return calculator.Calculate(original[stat], additions[stat], multiplications[stat]);
		}

		private StatsMath() { }

		/// <summary>
		/// Initializes a new <see cref="StatsMath"/> that will calculate using the specified stat operands.
		/// </summary>
		/// <param name="original">The original set of stats to use.</param>
		/// <param name="additions">A set of values to add to each stat.</param>
		/// <param name="multiplications">A set of values to multiply each stat.</param>
		public StatsMath(IStats original, IStats additions, IStats multiplications)
		{
			this.original = original;
			this.additions = additions;
			this.multiplications = multiplications;
		}

		// Defines the main calculation to be used for stat math
		int IStatsMath.Calculate(int a, int b, int m)
		{
			return (a + b).Scale(m);
		}
	}
}
