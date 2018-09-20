using System;
using Davfalcon.Revelator.Engine.Combat;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon.Revelator.UnitTests
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

		private static Weapon MakeWeapon()
			=> new Weapon()
			{
				BaseDamage = 5
			};

		/*
		[TestMethod]
		public void InitializeHPMP()
		{
			IUnit unit = MakeUnit();

			combat.Initialize(unit);

			Assert.AreEqual(unit.Stats[CombatStats.HP], unit.CombatProperties.CurrentHP);
			Assert.AreEqual(unit.Stats[CombatStats.MP], unit.CombatProperties.CurrentMP);
		}
		*/

		[TestMethod]
		public void CalculateAttackDamage()
		{
			ICombatResolver combat = CombatResolver.Default;
			IUnit unit = MakeUnit();

			Damage d = combat.CalculateOutgoingDamage(unit, MakeWeapon());

			Assert.AreEqual(5, d.Value);
			Assert.AreEqual(unit.Name, d.Source);
		}

		[TestMethod]
		public void CalculateAttackDamage_WithBonus()
		{
			ICombatResolver combat = CombatResolver.Default;
			IUnit unit = MakeUnit();

			Weapon weapon = MakeWeapon();
			weapon.BonusDamageStat = Attributes.STR;

			Damage d = combat.CalculateOutgoingDamage(unit, weapon);

			Assert.AreEqual(20, d.Value);
			Assert.AreEqual(unit.Name, d.Source);
		}

		[TestMethod]
		public void CalculateAttackDamage_WithScaling()
		{
			ICombatResolver combat = new CombatResolver.Builder().AddDamageScaling(DamageType.Physical, CombatStats.ATK).Build();
			IUnit unit = MakeUnit();

			Weapon weapon = MakeWeapon();
			weapon.BonusDamageStat = Attributes.STR;
			weapon.DamageTypes.Add(DamageType.Physical);

			Damage d = combat.CalculateOutgoingDamage(unit, weapon);

			Assert.AreEqual(24, d.Value);
			Assert.AreEqual(unit.Name, d.Source);
		}

		[TestMethod]
		public void CalculateReceivedDamage()
		{
			ICombatResolver combat = CombatResolver.Default;
			IUnit unit = MakeUnit();

			Damage d = new Damage(10, "", DamageType.Physical);

			Assert.AreEqual(10, combat.CalculateReceivedDamage(unit, d));
		}

		[TestMethod]
		public void CalculateReceivedDamage_WithResist()
		{
			ICombatResolver combat = new CombatResolver.Builder().AddDamageResist(DamageType.Physical, CombatStats.DEF).Build();
			IUnit unit = MakeUnit();

			Damage d = new Damage(10, "", DamageType.Physical);

			Assert.AreEqual(8, combat.CalculateReceivedDamage(unit, d));
		}
		/*
		[TestMethod]
		public void ReceiveDamage()
		{
			IUnit unit = MakeUnit();
			combat.Initialize(unit);

			Damage d = new Damage(DamageType.Physical, Element.Neutral, 10, "");
			HPLoss h = combat.ReceiveDamage(unit, d);

			Assert.AreEqual(unit.Stats[CombatStats.HP] - unit.CombatProperties.CurrentHP, h.Value);
			Assert.AreEqual(unit.Name, h.Unit);
		}
		*/
	}
}
