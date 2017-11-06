using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGLibrary;
using static RPGLibraryUnitTests.TestConstants;

namespace RPGLibraryUnitTests
{
	[TestClass]
	public class BasicUnitTests
	{
		private BasicUnit CreateTestUnit()
		{
			return new BasicUnit
			{
				Name = NAME,
				Class = CLASS,
				Level = LEVEL
			};
		}

		[TestMethod]
		public void BasicUnitInterfaceTest()
		{
			IUnit unit = CreateTestUnit();

			Assert.AreEqual(NAME, unit.Name);
			Assert.AreEqual(CLASS, unit.Class);
			Assert.AreEqual(LEVEL, unit.Level);
			Assert.IsNotNull(unit.Stats);
		}

		[TestMethod]
		public void BasicUnitStatsTest()
		{
			BasicUnit unit = CreateTestUnit();
			unit.BaseStats.Set(STAT_NAME, STAT_VALUE);

			Assert.AreEqual(STAT_VALUE, unit.Stats[STAT_NAME]);
			Assert.AreEqual(STAT_VALUE, unit.StatsDetails.Base[STAT_NAME]);
			Assert.AreEqual(STAT_VALUE, unit.StatsDetails.Final[STAT_NAME]);
		}
	}
}
