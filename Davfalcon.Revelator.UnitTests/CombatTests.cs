﻿using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Revelator.Engine.Combat;
using Davfalcon.Revelator.Borger;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon.Revelator.UnitTests
{
	[TestClass]
	public class CombatTests
	{
		private static IUnit MakeUnit()
			=> new Unit.Builder()
				.SetBaseStats(Enum.GetValues(typeof(Attributes)), 10)
				.SetBaseStat(Attributes.STR, 15)
				.SetBaseStat(Attributes.VIT, 15)
				.SetBaseStat(CombatStats.ATK, 20)
				.SetBaseStat(CombatStats.DEF, 20)
				.SetBaseStat(CombatStats.HP, 100)
				.SetBaseStat(CombatStats.MP, 100)
				.Build();

		[TestMethod]
		public void InitializeHPMP()
		{
			ICombatResolver combat = new CombatResolver.Builder()
				.AddVolatileStat(CombatStats.HP)
				.AddVolatileStat(CombatStats.MP)
				.Build();
			IUnit unit = MakeUnit();

			combat.Initialize(unit);

			Assert.AreEqual(unit.Stats[CombatStats.HP], unit.CombatProperties.VolatileStats[CombatStats.HP]);
			Assert.AreEqual(unit.Stats[CombatStats.MP], unit.CombatProperties.VolatileStats[CombatStats.MP]);
		}

		[TestMethod]
		public void CalculateAttackDamage()
		{
			ICombatResolver combat = CombatResolver.Default;
			IUnit unit = MakeUnit();

			IWeapon weapon = new Weapon.Builder(EquipmentType.Weapon, WeaponType.Sword)
				.SetDamage(5)
				.Build();

			Damage d = combat.CalculateOutgoingDamage(unit, weapon);

			Assert.AreEqual(5, d.Value);
			Assert.AreEqual(unit.Name, d.Source);
		}

		[TestMethod]
		public void CalculateAttackDamage_WithBonus()
		{
			ICombatResolver combat = CombatResolver.Default;
			IUnit unit = MakeUnit();

			IWeapon weapon = new Weapon.Builder(EquipmentType.Weapon, WeaponType.Sword)
				.SetDamage(5, Attributes.STR)
				.Build();

			Damage d = combat.CalculateOutgoingDamage(unit, weapon);

			Assert.AreEqual(20, d.Value);
			Assert.AreEqual(unit.Name, d.Source);
		}

		[TestMethod]
		public void CalculateAttackDamage_WithScaling()
		{
			ICombatResolver combat = new CombatResolver.Builder()
				.AddDamageScaling(DamageType.Physical, CombatStats.ATK)
				.Build();
			IUnit unit = MakeUnit();

			IWeapon weapon = new Weapon.Builder(EquipmentType.Weapon, WeaponType.Sword)
				.SetDamage(5, Attributes.STR)
				.AddDamageType(DamageType.Physical)
				.Build();

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
			ICombatResolver combat = new CombatResolver.Builder()
				.AddDamageResist(DamageType.Physical, CombatStats.DEF)
				.Build();
			IUnit unit = MakeUnit();

			Damage d = new Damage(10, "", DamageType.Physical);

			Assert.AreEqual(8, combat.CalculateReceivedDamage(unit, d));
		}
		
		[TestMethod]
		public void ReceiveDamage()
		{
			ICombatResolver combat = new CombatResolver.Builder()
				.AddDamageResourceRule(DamageType.Physical, CombatStats.HP)
				.AddVolatileStat(CombatStats.HP)
				.Build();
			IUnit unit = MakeUnit();
			combat.Initialize(unit);

			Damage d = new Damage(10, "", DamageType.Physical);
			IEnumerable<PointLoss> h = combat.ReceiveDamage(unit, d);

			Assert.AreEqual(unit.Stats[CombatStats.HP] - unit.CombatProperties.VolatileStats[CombatStats.HP], h.First().Value);
			Assert.AreEqual(unit.Name, h.First().Unit);
		}
	}
}
