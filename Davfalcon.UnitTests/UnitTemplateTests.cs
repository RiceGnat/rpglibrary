using Davfalcon.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon
{
    [TestClass]
    public class UnitTemplateTests
    {
		private enum TestStats { StatA, StatB }

		private const string UNIT_NAME = "TestUnit";

        private IUnit unit;

        private class TestUnit : UnitTemplate<IUnit>, IUnit
        {
            protected override IUnit Self => this;

            public TestUnit(string name)
            {
                Name = name;
            }
        }

        private class TestModifier : UnitModifier<IUnit>, IUnit
        {
            protected override IUnit Self => this;
        }

        [TestInitialize]
        public void Setup()
        {
            unit = new TestUnit(UNIT_NAME);
        }

        [TestMethod]
        public void Name()
        {
            Assert.AreEqual(UNIT_NAME, unit.Name);
        }

        [TestMethod]
        public void Link()
        {
            Assert.AreEqual(unit, unit.Modifiers.Target);
		}

		[TestMethod]
		public void Stats()
		{
			TestUnit unit = new TestUnit(UNIT_NAME);
			unit.BaseStats[TestStats.StatA] = 5;
			unit.StatDependencies[TestStats.StatB] = stats => stats[TestStats.StatA] * 2;

			Assert.AreEqual(5, unit.Stats[TestStats.StatA]);
			Assert.AreEqual(10, unit.Stats[TestStats.StatB]);
		}
	}
}
