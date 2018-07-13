﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Saffron.UnitTests.TestConstants;

namespace Saffron.UnitTests
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
		public void Interface()
		{
			IUnit unit = CreateTestUnit();

			Assert.AreEqual(NAME, unit.Name);
			Assert.AreEqual(CLASS, unit.Class);
			Assert.AreEqual(LEVEL, unit.Level);
			Assert.IsNotNull(unit.Stats);
		}

		[TestMethod]
		public void Stats()
		{
			BasicUnit unit = CreateTestUnit();
			unit.BaseStats.Set(STAT_NAME, STAT_VALUE);

			Assert.AreEqual(STAT_VALUE, unit.Stats[STAT_NAME]);
			Assert.AreEqual(STAT_VALUE, unit.StatsDetails.Base[STAT_NAME]);
			Assert.AreEqual(STAT_VALUE, unit.StatsDetails.Final[STAT_NAME]);
		}
	}
}