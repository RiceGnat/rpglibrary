using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Nodes;
using Davfalcon.Revelator.Borger;
using Davfalcon.Revelator.Combat;
using Davfalcon.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Davfalcon.Revelator.UnitTests
{
	[TestClass]
	public class CombatTests
	{
		private static IUnit MakeUnit()
			=> Unit.Build(b => b
				.SetMainDetails("Name")
				.SetBaseStat(Attributes.STR, 15)
				.SetBaseStat(Attributes.VIT, 15)
				.SetBaseStat(CombatStats.ATK, 20)
				.SetBaseStat(CombatStats.DEF, 20)
				.SetBaseStat(CombatStats.HP, 100)
				.SetBaseStat(CombatStats.MP, 100));

		[TestMethod]
		public void InitializeVolatileStats()
		{
			ICombatResolver combat = new CombatResolver.Builder()
				.AddVolatileStat(CombatStats.HP)
				.AddVolatileStat(CombatStats.MP)
				.Build();
			IUnit unit = MakeUnit();

			combat.Initialize(unit);

			Assert.AreEqual(unit.Stats[CombatStats.HP], unit.VolatileStats[CombatStats.HP]);
			Assert.AreEqual(unit.Stats[CombatStats.MP], unit.VolatileStats[CombatStats.MP]);
		}

		[TestMethod]
		public void CalculateAttackDamage()
		{
			ICombatResolver combat = CombatResolver.Default;
			IUnit unit = MakeUnit();

			IWeapon weapon = Weapon.Build(EquipmentType.Weapon, WeaponType.Sword, b => b
				.SetDamage(5));

			IDamageNode d = combat.GetDamageNode(unit, weapon);

			Assert.AreEqual(5, d.Value);
			Assert.AreEqual(unit.Name, d.Unit.Name);
		}

		[TestMethod]
		public void CalculateAttackDamage_WithBonus()
		{
			ICombatResolver combat = CombatResolver.Default;
			IUnit unit = MakeUnit();

			IWeapon weapon = Weapon.Build(EquipmentType.Weapon, WeaponType.Sword, b => b
				.SetDamage(5)
				.SetBonusDamageStat(Attributes.STR));

			IDamageNode d = combat.GetDamageNode(unit, weapon);

			Assert.AreEqual(20, d.Value);
			Assert.AreEqual(unit.Name, d.Unit.Name);
		}

		[TestMethod]
		public void CalculateAttackDamage_WithScaling()
		{
			ICombatResolver combat = new CombatResolver.Builder()
				.AddDamageScaling(DamageType.Physical, CombatStats.ATK)
				.Build();
			IUnit unit = MakeUnit();

			IWeapon weapon = Weapon.Build(EquipmentType.Weapon, WeaponType.Sword, b => b
				.SetDamage(5)
				.SetBonusDamageStat(Attributes.STR)
				.AddDamageType(DamageType.Physical));

			IDamageNode d = combat.GetDamageNode(unit, weapon);

			Assert.AreEqual(24, d.Value);
			Assert.AreEqual(unit.Name, d.Unit.Name);
		}

		[TestMethod]
		public void CalculateReceivedDamage()
		{
			ICombatResolver combat = CombatResolver.Default;
			IUnit unit = MakeUnit();

			var s = new Mock<IDamageSource>();
			s.Setup(m => m.DamageTypes).Returns(Enumerable.Empty<Enum>());

			var d = new Mock<IDamageNode>();
			d.Setup(m => m.Value).Returns(10);
			d.Setup(m => m.Source).Returns(s.Object);

			Assert.AreEqual(10, combat.GetDefenseNode(unit, d.Object).Value);
		}

		[TestMethod]
		public void CalculateReceivedDamage_WithResist()
		{
			ICombatResolver combat = new CombatResolver.Builder()
				.AddDamageResist(DamageType.Physical, CombatStats.DEF)
				.Build();
			IUnit unit = MakeUnit();

			var s = new Mock<IDamageSource>();
			s.Setup(m => m.DamageTypes).Returns(new List<Enum>() { DamageType.Physical });

			var d = new Mock<IDamageNode>();
			d.Setup(m => m.Value).Returns(10);
			d.Setup(m => m.Source).Returns(s.Object);

			Assert.AreEqual(8, combat.GetDefenseNode(unit, d.Object).Value);
		}

		//[TestMethod]
		//public void ReceiveDamage()
		//{
		//	ICombatResolver combat = new CombatResolver.Builder()
		//		.AddDamageResourceRule(DamageType.Physical, CombatStats.HP)
		//		.AddVolatileStat(CombatStats.HP)
		//		.Build();
		//	IUnit unit = MakeUnit();
		//	combat.Initialize(unit);

		//	Damage d = new Damage(10, "", DamageType.Physical);
		//	IEnumerable<StatChange> h = combat.ReceiveDamage(unit, d);

		//	Assert.AreEqual(unit.Stats[CombatStats.HP] - unit.VolatileStats[CombatStats.HP], -h.First().Value);
		//	Assert.AreEqual(unit.Name, h.First().Unit);
		//}

		[TestMethod]
		public void ApplyBuff()
		{
			ICombatResolver combat = new CombatResolver.Builder()
				.Build();

			IUnit unit = MakeUnit();
			IBuff b = new Buff.Builder()
				.SetName("Test buff")
				.SetStatAddition(CombatStats.ATK, 10)
				.Build();

			combat.ApplyBuff(unit, b);

			Assert.AreEqual(30, unit.Stats[CombatStats.ATK]);
		}

		[TestMethod]
		public void SerializeVolatileStats()
		{
			ICombatResolver combat = new CombatResolver.Builder()
				.AddVolatileStat(CombatStats.HP)
				.AddVolatileStat(CombatStats.MP)
				.Build();
			IUnit unit = MakeUnit();

			combat.Initialize(unit);

			IUnit clone = unit.DeepClone();

			Assert.AreEqual(unit.VolatileStats[CombatStats.HP], clone.VolatileStats[CombatStats.HP]);
			Assert.AreEqual(unit.VolatileStats[CombatStats.MP], clone.VolatileStats[CombatStats.MP]);
		}
	}
}
