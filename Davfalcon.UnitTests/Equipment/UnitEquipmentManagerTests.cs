﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon.Equipment
{
	[TestClass]
	public class UnitEquipmentManagerTests
	{
		private IUnit unit;

		private enum TestStats { Default }
		private enum ModType { Add }
		private enum UnitComponents { Equipment }
		private enum EquipmentType { TypeA, TypeB }

		private class TestUnit : UnitTemplate<IUnit>, IUnit
		{
			protected override IUnit Self => this;

			public TestUnit()
			{
				BaseStats[TestStats.Default] = 10;
			}
		}

		private interface IEquipment : IEquipment<IUnit, EquipmentType> { }

		private class TestEquipment : Equipment<IUnit, EquipmentType>, IEquipment, IUnit
		{
			protected override IUnit SelfAsUnit => this;

			protected override int Resolve(int baseValue, IReadOnlyDictionary<Enum, int> modifications)
				=> modifications[ModType.Add] + baseValue;

			protected override Func<int, int, int> GetAggregator(Enum modificationType) => (a, b) => a + b;

			protected override int GetAggregatorSeed(Enum modificationType) => 0;

			public TestEquipment(int add)
			{
				AddStatModificationType(ModType.Add);
				StatModifications[ModType.Add][TestStats.Default] = add;
			}
		}

		private class ConcreteEquipmentManager : UnitEquipmentManager<IUnit, EquipmentType, IEquipment> { }

		private ConcreteEquipmentManager GetEquipmentManager()
			=> unit.GetComponent<ConcreteEquipmentManager>(UnitComponents.Equipment);

		[TestInitialize]
		public void Setup()
		{
			TestUnit unit = new TestUnit();
			unit.AddComponent(UnitComponents.Equipment, new ConcreteEquipmentManager());
			this.unit = unit;
		}

		[TestMethod]
		public void Initialization()
		{
			Assert.IsNotNull(GetEquipmentManager());
		}

		[TestMethod]
		public void AddEquipmentSlot()
		{
			GetEquipmentManager().AddEquipmentSlot(EquipmentType.TypeA);
			GetEquipmentManager().AddEquipmentSlot(EquipmentType.TypeA);
			GetEquipmentManager().AddEquipmentSlot(EquipmentType.TypeB);

			Assert.AreEqual(EquipmentType.TypeA, GetEquipmentManager().EquipmentSlots[0].Type);
			Assert.AreEqual(EquipmentType.TypeA, GetEquipmentManager().EquipmentSlots[1].Type);
			Assert.AreEqual(EquipmentType.TypeB, GetEquipmentManager().EquipmentSlots[2].Type);
		}

		[TestMethod]
		public void Equip()
		{
			GetEquipmentManager().AddEquipmentSlot(EquipmentType.TypeA);

			IEquipment equipment = new TestEquipment(5);
			GetEquipmentManager().Equip(equipment);

			Assert.AreEqual(15, unit.Modifiers.AsModified().Stats[TestStats.Default]);
			Assert.AreEqual(equipment, GetEquipmentManager().GetEquipmentOfType(EquipmentType.TypeA));
		}

		[TestMethod]
		public void Unequip()
		{
			GetEquipmentManager().AddEquipmentSlot(EquipmentType.TypeA);

			IEquipment equipment = new TestEquipment(5);
			GetEquipmentManager().Equip(equipment);
			GetEquipmentManager().UnequipSlot(EquipmentType.TypeA);

			Assert.AreEqual(10, unit.Modifiers.AsModified().Stats[TestStats.Default]);
		}

		[TestMethod]
		public void EquipToSlotIndex()
		{
			GetEquipmentManager().AddEquipmentSlot(EquipmentType.TypeB);
			GetEquipmentManager().AddEquipmentSlot(EquipmentType.TypeA);
			GetEquipmentManager().AddEquipmentSlot(EquipmentType.TypeA);

			IEquipment equipment = new TestEquipment(5);
			GetEquipmentManager().EquipToSlotAt(2, equipment);

			Assert.AreEqual(15, unit.Modifiers.AsModified().Stats[TestStats.Default]);
			Assert.IsNull(GetEquipmentManager().GetEquipmentOfType(EquipmentType.TypeA, 0));
			Assert.AreEqual(equipment, GetEquipmentManager().GetEquipmentOfType(EquipmentType.TypeA, 1));
		}
	}
}
