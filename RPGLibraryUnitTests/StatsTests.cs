using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGLibrary;
using static RPGLibraryUnitTests.TestConstants;

namespace RPGLibraryUnitTests
{
	[TestClass]
	public class StatsTests
	{
		[TestMethod]
		public void StatsMapTest()
		{
			IStats stats = new StatsMap()
				.Set(STAT_NAME, STAT_VALUE);

			Assert.AreEqual(STAT_VALUE, stats.Get(STAT_NAME));
			Assert.AreEqual(STAT_VALUE, stats[STAT_NAME]);
		}

		[TestMethod]
		public void StatsMathTest()
		{
			IStats stats = new StatsMath(
				new StatsMap().Set(STAT_NAME, STAT_VALUE),
				new StatsMap().Set(STAT_NAME, STAT_VALUE),
				new StatsMap().Set(STAT_NAME, STAT_MULT));

			int expected = (STAT_VALUE + STAT_VALUE) * (int)(1 + STAT_MULT / 100f);

			Assert.AreEqual(expected, stats[STAT_NAME]);
			Assert.AreEqual(expected, ((IStatsMath)stats).Calculate(STAT_VALUE, STAT_VALUE, STAT_MULT));
		}

		[TestMethod]
		public void StatsConstantTest()
		{
			Assert.AreEqual(0, StatsConstant.Zero[STAT_NAME]);
			Assert.AreEqual(1, StatsConstant.One[STAT_NAME]);
		}
	}
}
