using Davfalcon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGLibrary;

namespace DavfalconUnitTests
{
	[TestClass]
	public class UnitStatsTests
	{
		private IUnit unit;

		[TestInitialize]
		public void GenerateUnit()
		{
			unit = TestObjects.GenerateBaselineUnit();
		}

		[TestMethod]
		public void HP()
		{
			Assert.AreEqual(375, unit.Stats[CombatStats.HP]);
		}

		[TestMethod]
		public void MP()
		{
			Assert.AreEqual(100, unit.Stats[CombatStats.MP]);
		}

		[TestMethod]
		public void ATK()
		{
			Assert.AreEqual(20, unit.Stats[CombatStats.ATK]);
		}

		[TestMethod]
		public void DEF()
		{
			Assert.AreEqual(20, unit.Stats[CombatStats.DEF]);
		}

		[TestMethod]
		public void MAG()
		{
			Assert.AreEqual(10, unit.Stats[CombatStats.MAG]);
		}

		[TestMethod]
		public void RES()
		{
			Assert.AreEqual(10, unit.Stats[CombatStats.RES]);
		}
	}
}
