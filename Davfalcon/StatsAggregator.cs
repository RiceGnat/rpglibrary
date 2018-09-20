namespace Davfalcon
{
	public class StatsAggregator : StatsPrototype
	{
		private readonly IStatsMath calculator;

		private readonly IStats a;
		private readonly IStats b;

		public override int Get(string stat)
		{
			return calculator.Aggregate(a[stat], b[stat]);
		}

		private StatsAggregator() { }

		public StatsAggregator(IStats a, IStats b, IStatsMath calculator)
		{
			this.a = a;
			this.b = b;

			this.calculator = calculator ?? StatsMath.Default;
		}

		public StatsAggregator(IStats a, IStats b)
			: this(a, b, null)
		{ }
	}
}
