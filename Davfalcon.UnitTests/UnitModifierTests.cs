using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Davfalcon.UnitTests.TestConstants;

namespace Davfalcon.UnitTests
{
	[TestClass]
	public class UnitModifierTests
	{
		private IUnit unit;

		[TestInitialize]
		public void GenerateUnit()
		{
			this.unit = BasicUnit.Build(u =>
			{
				u.BaseStats[STAT_NAME] = 10;
				return u;
			});
		}

		[DataTestMethod]
		[DataRow(10, 0, 20)]
		[DataRow(0, 20, 12)]
		[DataRow(10, 20, 24)]
		public void UnitStatsModifier(int add, int mult, int expected)
		{
			UnitStatsModifier modifier = new UnitStatsModifier();

			modifier.Additions[STAT_NAME] = add;
			modifier.Multipliers[STAT_NAME] = mult;

			unit.Modifiers.Add(modifier);

			Assert.AreEqual(expected, unit.Stats[STAT_NAME]);
			Assert.AreEqual(10, unit.StatsDetails.Base[STAT_NAME]);
		}

		[TestMethod]
		public void MultiplerModifiers()
		{
			UnitStatsModifier modifier = new UnitStatsModifier();

			modifier.Additions[STAT_NAME] = 10;
			modifier.Multipliers[STAT_NAME] = 20;

			unit.Modifiers.Add(modifier);

			Assert.AreEqual(24, unit.Stats[STAT_NAME]);

			modifier = new UnitStatsModifier();

			modifier.Additions[STAT_NAME] = 5;
			modifier.Multipliers[STAT_NAME] = 80;

			unit.Modifiers.Add(modifier);

			Assert.AreEqual(50, unit.Stats[STAT_NAME]);

			Assert.AreEqual(unit.Stats[STAT_NAME], unit.StatsDetails.Final[STAT_NAME]);
			Assert.AreEqual(10, unit.StatsDetails.Base[STAT_NAME]);
		}

		[TestMethod]
		public void RemoveModifier()
		{
			UnitStatsModifier modifier1 = new UnitStatsModifier();
			UnitStatsModifier modifier2 = new UnitStatsModifier();
			UnitStatsModifier modifier3 = new UnitStatsModifier();

			modifier1.Additions[STAT_NAME] = 1;
			modifier2.Additions[STAT_NAME] = 2;
			modifier3.Additions[STAT_NAME] = 3;

			unit.Modifiers.Add(modifier1);
			unit.Modifiers.Add(modifier2);
			unit.Modifiers.Add(modifier3);

			Assert.AreEqual(16, unit.Stats[STAT_NAME]);

			unit.Modifiers.Remove(modifier2);

			Assert.AreEqual(14, unit.Stats[STAT_NAME]);
		}
	}
}
