using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Davfalcon.UnitTests.TestConstants;

namespace Davfalcon.UnitTests
{
	[TestClass]
	public class BasicUnitTests
	{

		[TestMethod]
		public void Interface()
		{
			IUnit unit = BasicUnit.Build(u =>
			{
				u.Name = NAME;
				u.Class = CLASS;
				u.Level = LEVEL;
				return u;
			});

			Assert.AreEqual(NAME, unit.Name);
			Assert.AreEqual(CLASS, unit.Class);
			Assert.AreEqual(LEVEL, unit.Level);
			Assert.IsNotNull(unit.Stats);
		}

		[TestMethod]
		public void Stats()
		{
			IUnit unit = BasicUnit.Build(u =>
			{
				u.BaseStats.Set(STAT_NAME, STAT_VALUE);
				return u;
			});

			Assert.AreEqual(STAT_VALUE, unit.Stats[STAT_NAME]);
			Assert.AreEqual(STAT_VALUE, unit.StatsDetails.Base[STAT_NAME]);
			Assert.AreEqual(STAT_VALUE, unit.StatsDetails.Final[STAT_NAME]);
		}
	}
}
