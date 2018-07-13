namespace Saffron
{
	/// <summary>
	/// Performs math across a set of stats.
	/// </summary>
	public class StatsMath : StatsPrototype, IStatsMath
	{
		private static IStatsMath calculator = new StatsMath();
		public static IStatsMath Calculator { set { calculator = value; } }

		private IStats original;
		private IStats additions;
		private IStats multiplications;

		public override int Get(string stat)
		{
			return calculator.Calculate(original[stat], additions[stat], multiplications[stat]);
		}

		private StatsMath() { }

		public StatsMath(IStats original, IStats additions, IStats multiplications)
		{
			this.original = original;
			this.additions = additions;
			this.multiplications = multiplications;
		}

		int IStatsMath.Calculate(int a, int b, int m)
		{
			return (a + b).Scale(m);
		}
	}
}
