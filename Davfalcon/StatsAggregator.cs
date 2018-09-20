namespace Davfalcon
{
	public class StatsAggregator : StatsPrototype
	{
		private readonly IStatsResolver calculator;

		private readonly IStats a;
		private readonly IStats b;

		public override int Get(string stat)
		{
			return calculator.Aggregate(a[stat], b[stat]);
		}

		private StatsAggregator() { }

		public StatsAggregator(IStats a, IStats b, IStatsResolver calculator)
		{
			this.a = a;
			this.b = b;

			this.calculator = calculator ?? StatsResolver.Default;
		}

		public StatsAggregator(IStats a, IStats b)
			: this(a, b, null)
		{ }
	}
}
