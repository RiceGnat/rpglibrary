using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Davfalcon.Revelator.Borger;
using Davfalcon.Revelator.Engine.Combat;
using System.Collections.Generic;

namespace Davfalcon.Revelator.UnitTests.IntegrationTests
{
	[TestClass]
	public class CombatTests
	{
		private const string UNIT_NAME = "Test unit";
		private const string TARGET_NAME = "Test dummy";
		private const string WEAPON_NAME = "Test weapon";

		private static ICombatResolver combat;
		private IUnit unit;
		private IUnit target;

		[ClassInitialize]
		public static void Setup(TestContext context)
			=> combat = new CombatResolver.Builder()
				.AddVolatileStat(CombatStats.HP)
				.AddVolatileStat(CombatStats.MP)
				.AddDamageScaling(DamageType.Physical, CombatStats.ATK)
				.AddDamageResist(DamageType.Physical, CombatStats.DEF)
				.SetDefaultDamageResource(CombatStats.HP)
				.Build();

		[TestInitialize]
		public void TestSetup()
		{
			unit = new Unit.Builder()
				.SetMainDetails(UNIT_NAME)
				.SetBaseStat(Attributes.STR, 15)
				.SetBaseStat(Attributes.VIT, 15)
				.SetBaseStat(CombatStats.ATK, 20)
				.SetBaseStat(CombatStats.DEF, 20)
				.SetBaseStat(CombatStats.HP, 100)
				.SetBaseStat(CombatStats.MP, 100)
				.Build();

			target = new Unit.Builder()
				.SetMainDetails(TARGET_NAME)
				.SetBaseStat(CombatStats.HP, 1000)
				.Build();

			unit.ItemProperties.AddEquipmentSlot(EquipmentType.Weapon);
			unit.ItemProperties.Equip(new Weapon.Builder(EquipmentType.Weapon, WeaponType.Sword)
				.SetName(WEAPON_NAME)
				.SetStatAddition(CombatStats.ATK, 10)
				.SetDamage(20, Attributes.STR)
				.AddDamageType(DamageType.Physical)
				.Build());

			unit.ItemProperties.AddEquipmentSlot(EquipmentType.Weapon);
			unit.ItemProperties.Equip(new Weapon.Builder(EquipmentType.Weapon, WeaponType.Spear)
				.SetName(WEAPON_NAME + " 2")
				.SetDamage(20, Attributes.VIT)
				.AddDamageType(DamageType.Physical)
				.AddOnHitEffect("Effect", args =>
				{
					int old = args.Owner.CombatProperties.VolatileStats[CombatStats.HP];
					args.Owner.CombatProperties.VolatileStats[CombatStats.HP] += (args as CombatEffectArgs).DamageDealt.Value;
					StatChange heal = new StatChange(args.Owner, CombatStats.HP, args.Owner.CombatProperties.VolatileStats[CombatStats.HP] - old);
					ActionResult result = new ActionResult(args.Owner, args.Owner, null, null, heal);
					return EffectResult.FromArgs(args as CombatEffectArgs, new List<ActionResult> { result });
				})
				.Build(), 1);
		}

		[TestMethod]
		public void Attack()
		{
			combat.Initialize(unit);
			combat.Initialize(target);
			AttackResult result = combat.Attack(unit, target, unit.ItemProperties.GetEquipment(EquipmentType.Weapon) as IWeapon);

			Assert.AreEqual(UNIT_NAME, result.Unit);
			Assert.AreEqual(TARGET_NAME, result.Target);
			Assert.AreEqual(WEAPON_NAME, result.Weapon);
			Assert.AreEqual(45, result.DamageDealt.Value);
			Assert.AreEqual(955, target.CombatProperties.VolatileStats[CombatStats.HP]);
		}

		[TestMethod]
		public void AttackWithEffects()
		{
			combat.Initialize(unit);
			combat.Initialize(target);
			unit.CombatProperties.VolatileStats[CombatStats.HP] = 50;
			AttackResult result = combat.Attack(unit, target, unit.ItemProperties.GetEquipment(EquipmentType.Weapon, 1) as IWeapon);

			Assert.AreEqual(UNIT_NAME, result.Unit);
			Assert.AreEqual(TARGET_NAME, result.Target);
			Assert.AreEqual(WEAPON_NAME + " 2", result.Weapon);
			Assert.AreEqual(45, result.DamageDealt.Value);
			Assert.AreEqual(955, target.CombatProperties.VolatileStats[CombatStats.HP]);
			Assert.IsNotNull(result.OnHitEffects.First());
			Assert.AreEqual(95, unit.CombatProperties.VolatileStats[CombatStats.HP]);
		}
	}
}
