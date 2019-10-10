using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon
{
    [TestClass]
    public class UnitStatsModifierTests
    {
        private enum TestStats { StatA, StatB }
        private enum ModType { Add, Multiply }

        private const int INITIAL_VALUE = 10;

        private IUnit unit;

        private class TestUnit : UnitTemplate<IUnit>, IUnit
        {
            protected override IUnit Self => this;

            public TestUnit(int initialStat)
            {
                BaseStats[TestStats.StatA] = initialStat;
            }
        }

        private class TestModifier : UnitStatsModifier<IUnit>, IUnit
        {
            protected override IUnit SelfAsUnit => this;

            protected override Func<int, int, int> GetAggregator(Enum type) => (a, b) => a + b;

            protected override int GetAggregatorSeed(Enum type)
            {
                switch (type)
                {
                    case ModType.Multiply: return 1;
                    default: return 0;
                }
            }

            protected override int Resolver(int baseValue, IDictionary<Enum, int> modifications)
                => modifications[ModType.Add] + baseValue * modifications[ModType.Multiply];
            
            public TestModifier(TestStats stat, int add, int multiply)
            {
                AddStatModificationType(ModType.Add);
                AddStatModificationType(ModType.Multiply);
                StatModifications[ModType.Add][stat] = add;
                StatModifications[ModType.Multiply][stat] = multiply;
			}
        }

        [TestInitialize]
        public void Setup()
        {
            unit = new TestUnit(INITIAL_VALUE);
        }

        [TestMethod]
        public void StatModification()
        {
            unit.Modifiers.Add(new TestModifier(TestStats.StatA, 1, 2));

            Assert.AreEqual(INITIAL_VALUE, unit.Modifiers.AsModified().Stats.Base[TestStats.StatA]);
            Assert.AreEqual(INITIAL_VALUE * 3 + 1, unit.Modifiers.AsModified().Stats[TestStats.StatA]);
        }

        [TestMethod]
        public void ResolutionOrder()
        {
            unit.Modifiers.Add(new TestModifier(TestStats.StatA, 1, 2));
            unit.Modifiers.Add(new TestModifier(TestStats.StatA, 0, 3));

            Assert.AreEqual(INITIAL_VALUE, unit.Modifiers.AsModified().Stats.Base[TestStats.StatA]);
            Assert.AreEqual(INITIAL_VALUE * (1 + 2 + 3) + 1, unit.Modifiers.AsModified().Stats[TestStats.StatA]);
        }

		[TestMethod]
		public void DerivedStatModification()
		{
			TestUnit unit = new TestUnit(INITIAL_VALUE);
			unit.StatDerivations[TestStats.StatB] = stats => stats[TestStats.StatA] * 2;
            TestModifier modA = new TestModifier(TestStats.StatA, 1, 2);
            TestModifier modB = new TestModifier(TestStats.StatB, 3, 1);

            unit.Modifiers.Add(modA);
            unit.Modifiers.Add(modB);

            Assert.AreEqual(INITIAL_VALUE, unit.Stats.Base[TestStats.StatA]);
            Assert.AreEqual(INITIAL_VALUE * 2, unit.Stats.Base[TestStats.StatB]);
            Assert.AreEqual(INITIAL_VALUE * 3 + 1, modA.AsModified().Stats[TestStats.StatA]);
            Assert.AreEqual((INITIAL_VALUE * 3 + 1) * 2, modA.AsModified().Stats[TestStats.StatB]);
            Assert.AreEqual(INITIAL_VALUE * 3 + 1, modB.AsModified().Stats[TestStats.StatA]);
            Assert.AreEqual((INITIAL_VALUE * 3 + 1) * 2 * 2 + 3, modB.AsModified().Stats[TestStats.StatB]);
        }
    }
}
