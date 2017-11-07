using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGLibrary;
using static RPGLibraryUnitTests.TestConstants;

namespace RPGLibraryUnitTests
{
	[TestClass]
	public class UnitModifierTests
	{
		private IUnit unit;

		[TestInitialize]
		public void GenerateUnit()
		{
			BasicUnit unit = new BasicUnit();
			unit.BaseStats[STAT_NAME] = 10;
			this.unit = unit;
		}

		[TestMethod]
		public void UnitStatsModifier()
		{
			UnitStatsModifier modifier = new UnitStatsModifier();

			modifier.Additions[STAT_NAME] = 10;
			modifier.Multiplications[STAT_NAME] = 20;

			unit.Modifiers.Add(modifier);

			Assert.AreEqual(24, unit.Stats[STAT_NAME]);

			modifier = new UnitStatsModifier();

			modifier.Additions[STAT_NAME] = 5;
			modifier.Multiplications[STAT_NAME] = 80;

			unit.Modifiers.Add(modifier);

			Assert.AreEqual(50, unit.Stats[STAT_NAME]);

			Assert.AreEqual(unit.Stats[STAT_NAME], unit.StatsDetails.Final[STAT_NAME]);
			Assert.AreEqual(10, unit.StatsDetails.Base[STAT_NAME]);
		}

		[TestMethod]
		public void TimedModifier()
		{
			TimedModifier modifier = new TimedModifier();
			modifier.Duration = 1;
			modifier.Reset();
			Assert.AreEqual(modifier.Duration, modifier.Remaining);
			modifier.Tick();
			Assert.AreEqual(0, modifier.Remaining);
			modifier.Tick();
			Assert.AreEqual(0, modifier.Remaining);
		}
	}
}
