﻿using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Davfalcon.Revelator.Borger;
using Davfalcon.Revelator.Combat;
using System.Collections.Generic;
using Davfalcon.Nodes;
using System.Diagnostics;

namespace Davfalcon.Revelator.UnitTests.IntegrationTests
{
	[TestClass]
	public class StatNodeTests
	{
		private const string UNIT_NAME = "Test unit";
		private const string TARGET_NAME = "Test dummy";
		private const CombatStats STAT = CombatStats.ATK;
		private const CombatStats DEF_STAT = CombatStats.DEF;

		private static ICombatResolver combat;

		[ClassInitialize]
		public static void Setup(TestContext context)
			=> combat = new CombatResolver.Builder()
				.AddVolatileStat(CombatStats.HP)
				.SetDefaultDamageResource(CombatStats.HP)
				.Build();

		private IUnit unit;
		private IUnit target;

		[TestInitialize]
		public void TestSetup()
		{
			unit = new Unit.Builder()
				.SetMainDetails(UNIT_NAME)
				.SetBaseStat(STAT, 15)
				.Build();

			unit.Equipment.AddEquipmentSlot(EquipmentType.Weapon);
			unit.Equipment.Equip(new Weapon.Builder(EquipmentType.Weapon, WeaponType.Sword)
				.SetName("Weapon")
				.SetStatAddition(STAT, 20)
				.SetDamage(20, STAT)
				.AddDamageType(DamageType.Physical)
				.Build());

			unit.Equipment.AddEquipmentSlot(EquipmentType.Armor);
			unit.Equipment.Equip(new Equipment.Builder(EquipmentType.Armor)
				.SetName("Armor")
				.SetStatAddition(STAT, 5)
				.Build());

			unit.Equipment.AddEquipmentSlot(EquipmentType.Accessory);
			unit.Equipment.Equip(new Equipment.Builder(EquipmentType.Accessory)
				.SetName("Accessory")
				.SetStatAddition(STAT, 5)
				.SetStatMultiplier(STAT, 20)
				.Build());

			target = new Unit.Builder()
				.SetMainDetails(TARGET_NAME)
				.SetBaseStat(CombatStats.DEF, 20)
				.SetBaseStat(CombatStats.HP, 1000)
				.Build();


			combat.Initialize(unit);
			combat.Initialize(target);
		}

		private class DamageCalculationNode : NodeEnumerableBase, INode
		{
			public override int Value => StatsOperations.Default.ScaleInverse(nodes[1].Value, nodes[2].Value);
			public override string Name => "Attack";

			private IUnit unit;
			private IUnit target;
			private IWeapon weapon;
			private readonly INode[] nodes = new INode[3];

			public DamageCalculationNode(IUnit unit, IUnit target, IWeapon weapon)
			{
				this.unit = unit;
				this.target = target;
				this.weapon = weapon;

				nodes[0] = new ConstantNode($"Base damage ({weapon.Name})", weapon.BaseDamage);
				nodes[1] = new ResolverNode($"Outgoing damage ({unit.Name})", nodes[0], ConstantNode.Zero, unit.StatsDetails.GetStatNode(STAT), StatsOperations.Default);
				nodes[2] = target.StatsDetails.GetStatNode(DEF_STAT);
			}

			public override string ToString()
				=> $"{unit.Name} deals {Value} damage to {target.Name} with {weapon.Name}";

			protected override IEnumerator<INode> GetEnumerator()
				=> (nodes as IEnumerable<INode>).GetEnumerator();
		}

		[TestMethod]
		public void StatNode()
		{
			IBuff buff = new Buff.Builder()
				.SetName("Debuff")
				.SetStatMultiplier(STAT, -50)
				.Build();

			combat.ApplyBuff(unit, buff);

			INode node = unit.StatsDetails.GetStatNode(STAT);

			Debug.Write(node.ToStringRecursive());

			Assert.AreEqual(34, node.Value);
		}

		[TestMethod]
		public void SampleDamageCalc()
		{
			INode node = new DamageCalculationNode(unit, target, unit.Equipment.GetEquipment(EquipmentType.Weapon) as IWeapon);

			Debug.Write(node.ToStringRecursive());

			Assert.AreEqual(25, node.Value);
		}
	}
}
