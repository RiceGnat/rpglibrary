using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon.Stats
{
    [TestClass]
	public class StatsTests
    {
        private enum TestEnum
        {
            TestStat
        }

        private const TestEnum STAT_NAME = TestEnum.TestStat;
        private const int STAT_VALUE = 10;
        private const int STAT_MULT = 100;

        [TestMethod]
		public void StatsMap()
		{
			IStats stats = new StatsMap()
				.Set(STAT_NAME, STAT_VALUE);

			Assert.AreEqual(STAT_VALUE, stats.Get(STAT_NAME));
			Assert.AreEqual(STAT_VALUE, stats[STAT_NAME]);
		}
	}
}
