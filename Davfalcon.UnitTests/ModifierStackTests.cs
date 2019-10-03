using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon.UnitTests
{
    [TestClass]
    public class ModifierStackTests
    {
        private const string UNIT_NAME = "TestUnit";

        private IUnit unit;

        private class TestUnit : IUnit
        {
            public string Name => UNIT_NAME;
            public IModifierStack<IUnit> Modifiers { get; } = new ModifierStack<IUnit>();

            #region Not implemented
            int IUnit.Level => throw new System.NotImplementedException();
            string IUnit.Class => throw new System.NotImplementedException();
            IStats IUnit.Stats => throw new System.NotImplementedException();
            IStatsPackage IUnit.StatsDetails => throw new System.NotImplementedException();
            #endregion

            public TestUnit()
            {
                Modifiers.Bind(this);
            }
        }

        private class TestModifier : UnitModifier, IUnit
        {
            private readonly string suffix;

            string IUnit.Name => Target.Name + suffix;

            public TestModifier(string suffix)
            {
                this.suffix = suffix;
            }
        }

        [TestInitialize]
        public void GenerateUnit()
        {
            unit = new TestUnit();
        }

        [TestMethod]
        public void Add()
        {
            unit.Modifiers.Add(new TestModifier("A"));
            unit.Modifiers.Add(new TestModifier("B"));

            Assert.AreEqual(UNIT_NAME + "AB", unit.Modifiers.AsModified().Name);
        }

        [DataTestMethod]
        [DataRow(0, "CAB")]
        [DataRow(1, "ACB")]
        [DataRow(2, "ABC")]
        public void Insert(int index, string result)
        {
            unit.Modifiers.Add(new TestModifier("A"));
            unit.Modifiers.Add(new TestModifier("B"));
            unit.Modifiers.Insert(index, new TestModifier("C"));

            Assert.AreEqual(UNIT_NAME + result, unit.Modifiers.AsModified().Name);
        }

        [DataTestMethod]
        [DataRow(0, "BC")]
        [DataRow(1, "AC")]
        [DataRow(2, "AB")]
        public void RemoveAt(int index, string result)
        {
            unit.Modifiers.Add(new TestModifier("A"));
            unit.Modifiers.Add(new TestModifier("B"));
            unit.Modifiers.Add(new TestModifier("C"));
            unit.Modifiers.RemoveAt(index);

            Assert.AreEqual(UNIT_NAME + result, unit.Modifiers.AsModified().Name);
        }

        [TestMethod]
        public void Remove()
        {
            IModifier<IUnit> modifierB = new TestModifier("B");
            unit.Modifiers.Add(new TestModifier("A"));
            unit.Modifiers.Add(modifierB);
            unit.Modifiers.Add(new TestModifier("C"));
            unit.Modifiers.Remove(modifierB);

            Assert.AreEqual(UNIT_NAME + "AC", unit.Modifiers.AsModified().Name);
        }
    }
}
