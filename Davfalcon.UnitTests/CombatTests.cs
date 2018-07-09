using System;
using Davfalcon.Engine.Combat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGLibrary;

namespace Davfalcon.UnitTests
{
	[TestClass]
	public class CombatTests
	{
		private ICombatEvaluator combat;

		[TestInitialize]
		public void Setup()
		{
			combat = new CombatEvaluator(null);
		}

		private static Unit MakeUnit()
		{
			Unit unit = new Unit();

			foreach (Attributes stat in Enum.GetValues(typeof(Attributes)))
			{
				unit.BaseStats[stat] = 10;
			}

			unit.BaseStats[Attributes.STR] = 15;
			unit.BaseStats[Attributes.VIT] = 15;

			return unit;
		}

		[TestMethod]
		public void InitializeHPMP()
		{
			IUnit unit = MakeUnit();

			combat.Initialize(unit);

			Assert.AreEqual(unit.Stats[CombatStats.HP], combat.GetCombatProperties(unit).CurrentHP);
			Assert.AreEqual(unit.Stats[CombatStats.MP], combat.GetCombatProperties(unit).CurrentMP);
		}

		[TestMethod]
		public void ScaleDamageValue()
		{
			Assert.AreEqual(12, combat.ScaleDamageValue(10, 20));
		}

		[TestMethod]
		public void MitigateDamageValue()
		{
			Assert.AreEqual(5, combat.MitigateDamageValue(10, 100));
		}

		[TestMethod]
		public void CalculateAttackDamage()
		{
			IUnit unit = MakeUnit();

			Damage d = combat.CalculateAttackDamage(unit);

			Assert.AreEqual(18, d.Value);
			Assert.AreEqual(unit.Name, d.Source);
		}

		[TestMethod]
		public void CalculateReceivedPhysicalDamage()
		{
			IUnit unit = MakeUnit();

			Damage d = new Damage(DamageType.Physical, Element.Neutral, 10, "");

			Assert.AreEqual(8, combat.CalculateReceivedDamage(unit, d));
		}

		[TestMethod]
		public void CalculateReceivedMagicalDamage()
		{
			IUnit unit = MakeUnit();

			Damage d = new Damage(DamageType.Magical, Element.Neutral, 10, "");

			Assert.AreEqual(9, combat.CalculateReceivedDamage(unit, d));
		}

		[TestMethod]
		public void CalculateReceivedTrueDamage()
		{
			IUnit unit = MakeUnit();

			Damage d = new Damage(DamageType.True, Element.Neutral, 10, "");

			Assert.AreEqual(10, combat.CalculateReceivedDamage(unit, d));
		}

		[TestMethod]
		public void ReceiveDamage()
		{
			IUnit unit = MakeUnit();
			combat.Initialize(unit);

			Damage d = new Damage(DamageType.Physical, Element.Neutral, 10, "");
			HPLoss h = combat.ReceiveDamage(unit, d);

			Assert.AreEqual(unit.Stats[CombatStats.HP] - combat.GetCombatProperties(unit).CurrentHP, h.Value);
			Assert.AreEqual(unit.Name, h.Unit);
		}
	}
}
