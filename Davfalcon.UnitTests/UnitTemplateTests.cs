using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon
{
    [TestClass]
    public class UnitTemplateTests
    {
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
            public override IUnit AsModified() => this;
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
    }
}
