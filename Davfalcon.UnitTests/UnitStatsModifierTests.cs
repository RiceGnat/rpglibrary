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
            protected override IUnit Self => this;

            protected override Func<int, int, int> GetAggregator(Enum type) => (a, b) => a + b;

            protected override int Resolver(int baseValue, IDictionary<Enum, int> modifications)
                => modifications[ModType.Add] + baseValue * modifications[ModType.Multiply];
            
            public TestModifier(int add, int multiply)
            {
                AddStatModificationType(ModType.Add);
                AddStatModificationType(ModType.Multiply);
                StatModifications[ModType.Add][TestStats.StatA] = add;
                StatModifications[ModType.Multiply][TestStats.StatA] = multiply;
				StatModifications[ModType.Add][TestStats.StatB] = add;
				StatModifications[ModType.Multiply][TestStats.StatB] = multiply;
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
            unit.Modifiers.Add(new TestModifier(1, 2));

            Assert.AreEqual(INITIAL_VALUE, unit.Modifiers.AsModified().Stats.Base[TestStats.StatA]);
            Assert.AreEqual(INITIAL_VALUE * 2 + 1, unit.Modifiers.AsModified().Stats[TestStats.StatA]);
        }

        [TestMethod]
        public void ResolutionOrder()
        {
            unit.Modifiers.Add(new TestModifier(1, 2));
            unit.Modifiers.Add(new TestModifier(0, 3));

            Assert.AreEqual(INITIAL_VALUE, unit.Modifiers.AsModified().Stats.Base[TestStats.StatA]);
            Assert.AreEqual(INITIAL_VALUE * (2 + 3) + 1, unit.Modifiers.AsModified().Stats[TestStats.StatA]);
        }

		[TestMethod]
		public void DependentStatModification()
		{
			TestUnit unit = new TestUnit(INITIAL_VALUE);
			unit.StatDependencies[TestStats.StatB] = stats => stats[TestStats.StatA] * 2;
			unit.Modifiers.Add(new TestModifier(1, 2));

			Assert.AreEqual(INITIAL_VALUE * 2, unit.Stats.Base[TestStats.StatB]);
			Assert.AreEqual((INITIAL_VALUE * 2 + 1) * 2 * 2 + 1, unit.Modifiers.AsModified().Stats[TestStats.StatB]);
		}
    }
}
