using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon.Equipment
{
	[TestClass]
	public class CompoundEquipmentTests
	{
		private enum TestStats { Default }
		private enum ModType { Add }
		private enum EquipmentType { Default }

		private class TestUnit : UnitTemplate<IUnit>, IUnit
		{
			protected override IUnit Self => this;

			public TestUnit()
			{
				BaseStats[TestStats.Default] = 10;
			}
		}

		private interface IEquipment : IEquipment<IUnit, EquipmentType> { }

		private class TestEquipment : CompoundEquipment<IUnit, EquipmentType>, IEquipment, IUnit
		{
			protected override IUnit SelfAsUnit => this;

			public TestEquipment(int add)
			{
				AddStatModificationType(ModType.Add);
				StatModifications[ModType.Add][TestStats.Default] = add;

				Resolver = (baseValue, modifications) => modifications[ModType.Add] + baseValue;
				GetAggregator = (type) => (a, b) => a + b;
				GetAggregatorSeed = (type) => 0;
			}
		}
		private class TestModifier : UnitStatsModifier<IUnit>, IUnit
		{
			protected override IUnit SelfAsUnit => this;

			public TestModifier(int add)
			{
				AddStatModificationType(ModType.Add);
				StatModifications[ModType.Add][TestStats.Default] = add;

				Resolver = (baseValue, modifications) => modifications[ModType.Add] + baseValue;
				GetAggregator = (type) => (a, b) => a + b;
				GetAggregatorSeed = (type) => 0;
			}
		}

		private IUnit unit;
		private IEquipment equipment;

		[TestInitialize]
		public void Setup()
		{
			unit = new TestUnit();

			TestEquipment equipment = new TestEquipment(3);
			equipment.Modifiers.Add(new TestModifier(2));
			equipment.Bind(unit);

			this.equipment = equipment;
		}

		[TestMethod]
		public void AsModified()
		{
			Assert.AreEqual(15, equipment.AsModified().Stats[TestStats.Default]);
		}
	}
}
