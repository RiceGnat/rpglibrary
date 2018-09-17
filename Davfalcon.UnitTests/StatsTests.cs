using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Davfalcon.UnitTests.TestConstants;

namespace Davfalcon.UnitTests
{
	[TestClass]
	public class StatsTests
	{
		[TestMethod]
		public void StatsMap()
		{
			IStats stats = new StatsMap()
				.Set(STAT_NAME, STAT_VALUE);

			Assert.AreEqual(STAT_VALUE, stats.Get(STAT_NAME));
			Assert.AreEqual(STAT_VALUE, stats[STAT_NAME]);
		}

		[TestMethod]
		public void StatsMath()
		{
			IStats stats = new StatsMath(
				new StatsMap().Set(STAT_NAME, STAT_VALUE),
				new StatsMap().Set(STAT_NAME, STAT_VALUE),
				new StatsMap().Set(STAT_NAME, STAT_MULT));

			int expected = (STAT_VALUE + STAT_VALUE) * (int)(1 + STAT_MULT / 100f);

			Assert.AreEqual(expected, stats[STAT_NAME]);
			Assert.AreEqual(expected, ((IStatsMath)stats).Calculate(STAT_VALUE, STAT_VALUE, STAT_MULT));
		}
	}
}
