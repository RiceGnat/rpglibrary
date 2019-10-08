using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon
{
    [TestClass]
	public class UnitModifierTests
	{
        private const string UNIT_NAME = "TestUnit";
        private const string MODIFIED_UNIT_NAME = "TestUnitModified";

        private IUnit unit;
        private IModifier<IUnit> modifier;

        private class TestUnit : IUnit
        {
            public string Name { get; }

            #region Not implemented
            IStatsProperties IUnitTemplate<IUnit>.Stats => throw new NotImplementedException();
            IModifierStack<IUnit> IUnitTemplate<IUnit>.Modifiers => throw new NotImplementedException();
            #endregion

            public TestUnit(string name)
            {
                Name = name;
            }
        }

        private class TestModifier : UnitModifier<IUnit>, IUnit
        {
            string IUnitTemplate<IUnit>.Name => MODIFIED_UNIT_NAME;
            public override IUnit AsModified() => this;
        }

        [TestInitialize]
		public void Setup()
		{
            unit = new TestUnit(UNIT_NAME);
            modifier = new TestModifier();
            modifier.Bind(unit);
        }

        [TestMethod]
        public void PropertyModification()
        {
            Assert.AreEqual(UNIT_NAME, unit.Name);
            Assert.AreEqual(MODIFIED_UNIT_NAME, modifier.AsModified().Name);
        }
	}
}
