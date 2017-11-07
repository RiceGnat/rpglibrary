using Davfalcon;
using Davfalcon.Combat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGLibrary;

namespace DavfalconUnitTests
{
	[TestClass]
	public class CombatTests
	{
		[TestMethod]
		public void InitializeHPMP()
		{
			IUnit unit = TestObjects.GenerateBaselineUnit();

			Combat.Initialize(unit);

			Assert.AreEqual(unit.Stats[CombatStats.HP], unit.GetCombatProps().CurrentHP);
			Assert.AreEqual(unit.Stats[CombatStats.MP], unit.GetCombatProps().CurrentMP);
		}

		[TestMethod]
		public void ScaleDamageValue()
		{
			Assert.AreEqual(12, Combat.ScaleDamageValue(10, 20));
		}

		[TestMethod]
		public void MitigateDamageValue()
		{
			Assert.AreEqual(5, Combat.MitigateDamageValue(10, 100));
		}

		[TestMethod]
		public void CalculateAttackDamage()
		{
			IUnit unit = TestObjects.GenerateBaselineUnit();

			Damage d = Combat.CalculateAttackDamage(unit);

			Assert.AreEqual(18, d.Value);
			Assert.AreEqual(unit.Name, d.Source);
		}

		[TestMethod]
		public void CalculateReceivedPhysicalDamage()
		{
			IUnit unit = TestObjects.GenerateBaselineUnit();

			Damage d = new Damage(DamageType.Physical, Element.Neutral, 10, "");

			Assert.AreEqual(8, Combat.CalculateReceivedDamage(unit, d));
		}

		[TestMethod]
		public void CalculateReceivedMagicalDamage()
		{
			IUnit unit = TestObjects.GenerateBaselineUnit();

			Damage d = new Damage(DamageType.Magical, Element.Neutral, 10, "");

			Assert.AreEqual(9, Combat.CalculateReceivedDamage(unit, d));
		}

		[TestMethod]
		public void CalculateReceivedTrueDamage()
		{
			IUnit unit = TestObjects.GenerateBaselineUnit();

			Damage d = new Damage(DamageType.True, Element.Neutral, 10, "");

			Assert.AreEqual(10, Combat.CalculateReceivedDamage(unit, d));
		}

		[TestMethod]
		public void ReceiveDamage()
		{
			IUnit unit = TestObjects.GenerateBaselineUnit();
			Combat.Initialize(unit);

			Damage d = new Damage(DamageType.Physical, Element.Neutral, 10, "");
			HPLoss h = Combat.ReceiveDamage(unit, d);

			Assert.AreEqual(unit.Stats[CombatStats.HP] - unit.GetCombatProps().CurrentHP, h.Value);
			Assert.AreEqual(unit.Name, h.Unit);
		}
	}
}
