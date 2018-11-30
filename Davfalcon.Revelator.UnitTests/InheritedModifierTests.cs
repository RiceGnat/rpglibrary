using System;
using Davfalcon.Revelator.Borger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon.Revelator.UnitTests
{
	[TestClass]
	public class InheritedModifierTests
	{
		private IUnit unit;

		[TestInitialize]
		public void Setup()
		{
			unit = Unit.Build(b => b);
		}

		private class UnitStatsModifierA : UnitStatsModifier<IUnit> { }

		[TestMethod]
		public void BaseInterfaceModifierAdd()
		{
			Davfalcon.IUnit unit = this.unit;
			unit.Modifiers.Add(Equipment.Build(EquipmentType.Armor, b => b) as IModifier<Davfalcon.IUnit>);
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void BaseInterfaceModifierAdd_NotSupportedException()
		{
			Davfalcon.IUnit unit = this.unit;
			unit.Modifiers.Add(new UnitStatsModifierA());
		}

		private class UnitStatsModifierB : UnitStatsModifier<Davfalcon.IUnit> { }

		[TestMethod]
		[ExpectedException(typeof(InvalidCastException))]
		public void BaseInterfaceModifierAdd_InvalidCastException()
		{
			Davfalcon.IUnit unit = this.unit;
			unit.Modifiers.Add(new UnitStatsModifierB());
		}

	}
}
