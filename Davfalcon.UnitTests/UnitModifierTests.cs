using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon.UnitTests
{
    [TestClass]
	public class UnitModifierTests
	{
        private const string UNIT_NAME = "TestUnit";
        private const string MODIFIER_NAME = "TestModifier";
        private const int UNIT_LEVEL = 1;

        private IUnit unit;
        private IModifier<IUnit> modifier;

        private class TestUnit : IUnit
        {
            public string Name { get; }
            public int Level => UNIT_LEVEL;

            #region Not implemented
            string IUnit.Class => throw new System.NotImplementedException();
            IStats IUnit.Stats => throw new System.NotImplementedException();
            IStatsPackage IUnit.StatsDetails => throw new System.NotImplementedException();
            IModifierStack<IUnit> IUnit.Modifiers => throw new System.NotImplementedException();
            #endregion

            public TestUnit(string name)
            {
                Name = name;
            }
        }

        private class TestModifier : UnitModifier, IUnit
        {
            int IUnit.Level => Target.Level + 1;

            public TestModifier(string name)
            {
                Name = name;
            }
        }

        [TestInitialize]
		public void GenerateUnit()
		{
            unit = new TestUnit(UNIT_NAME);
            modifier = new TestModifier(MODIFIER_NAME);
            modifier.Bind(unit);
        }

        [TestMethod]
        public void NameResolution()
        {
            INameable nameable = modifier;

            Assert.AreEqual(UNIT_NAME, unit.Name);
            Assert.AreEqual(MODIFIER_NAME, modifier.Name);
            Assert.AreEqual(MODIFIER_NAME, nameable.Name);
        }

        [TestMethod]
        public void PropertyModification()
        {
            Assert.AreEqual(UNIT_LEVEL, unit.Level);
            Assert.AreEqual(UNIT_LEVEL + 1, modifier.AsModified().Level);
        }
	}
}
