using System;
using Davfalcon.Combat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGLibrary;

namespace Davfalcon.UnitTests
{
	[TestClass]
	public class CombatTests
	{
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

			unit.Initialize();

			Assert.AreEqual(unit.Stats[CombatStats.HP], unit.GetCombatProps().CurrentHP);
			Assert.AreEqual(unit.Stats[CombatStats.MP], unit.GetCombatProps().CurrentMP);
		}

		[TestMethod]
		public void ScaleDamageValue()
		{
			Assert.AreEqual(12, CombatManager.ScaleDamageValue(10, 20));
		}

		[TestMethod]
		public void MitigateDamageValue()
		{
			Assert.AreEqual(5, CombatManager.MitigateDamageValue(10, 100));
		}

		[TestMethod]
		public void CalculateAttackDamage()
		{
			IUnit unit = MakeUnit();

			Damage d = unit.CalculateAttackDamage();

			Assert.AreEqual(18, d.Value);
			Assert.AreEqual(unit.Name, d.Source);
		}

		[TestMethod]
		public void CalculateReceivedPhysicalDamage()
		{
			IUnit unit = MakeUnit();

			Damage d = new Damage(DamageType.Physical, Element.Neutral, 10, "");

			Assert.AreEqual(8, unit.CalculateReceivedDamage(d));
		}

		[TestMethod]
		public void CalculateReceivedMagicalDamage()
		{
			IUnit unit = MakeUnit();

			Damage d = new Damage(DamageType.Magical, Element.Neutral, 10, "");

			Assert.AreEqual(9, unit.CalculateReceivedDamage(d));
		}

		[TestMethod]
		public void CalculateReceivedTrueDamage()
		{
			IUnit unit = MakeUnit();

			Damage d = new Damage(DamageType.True, Element.Neutral, 10, "");

			Assert.AreEqual(10, unit.CalculateReceivedDamage(d));
		}

		[TestMethod]
		public void ReceiveDamage()
		{
			IUnit unit = MakeUnit();
			unit.Initialize();

			Damage d = new Damage(DamageType.Physical, Element.Neutral, 10, "");
			HPLoss h = unit.ReceiveDamage(d);

			Assert.AreEqual(unit.Stats[CombatStats.HP] - unit.GetCombatProps().CurrentHP, h.Value);
			Assert.AreEqual(unit.Name, h.Unit);
		}
	}
}
