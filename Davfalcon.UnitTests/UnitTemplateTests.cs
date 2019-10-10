using System;
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

        [Serializable]
        private class TestUnit : UnitTemplate<IUnit>, IUnit
        {
            protected override IUnit Self => this;

            public TestUnit(string name)
            {
                Name = name;
            }
        }

        [Serializable]
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
			unit.StatDerivations[TestStats.StatB] = stats => stats[TestStats.StatA] * 2;

			Assert.AreEqual(5, unit.Stats[TestStats.StatA]);
			Assert.AreEqual(10, unit.Stats[TestStats.StatB]);
            Assert.AreEqual(10, unit.Stats.GetModificationBase(TestStats.StatB));
        }

        [TestMethod]
        public void StatsSerialization()
        {
            TestUnit unit = new TestUnit(UNIT_NAME);
            unit.BaseStats[TestStats.StatA] = 5;
            unit.StatDerivations[TestStats.StatB] = stats => stats[TestStats.StatA] * 2;

            Assert.AreEqual(5, unit.Stats.GetModificationBase(TestStats.StatA));
            Assert.AreEqual(10, unit.Stats.GetModificationBase(TestStats.StatB));

            // Ensure unit stats are deserialized correctly
            TestUnit clone = unit.DeepClone();
            clone.BaseStats[TestStats.StatA] = 10;

            Assert.AreEqual(10, clone.Stats.GetModificationBase(TestStats.StatA));
            Assert.AreEqual(20, clone.Stats.GetModificationBase(TestStats.StatB));
        }

        [TestMethod]
        public void ModifierSerialization()
        {
            unit.Modifiers.Add(new TestModifier());
            IUnit clone = unit.DeepClone();

            Assert.AreNotEqual(clone, clone.Modifiers.AsModified());
        }
    }
}
