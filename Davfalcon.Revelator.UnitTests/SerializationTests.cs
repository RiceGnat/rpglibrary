using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Davfalcon.Revelator.Borger;
using Davfalcon.Serialization;

namespace Davfalcon.Revelator.UnitTests
{
	[TestClass]
	public class SerializationTests
	{
		/*
		private static SystemData data;
		[ClassInitialize]
		public static void Load(TestContext context)
		{
			data = SystemData.Current;
			SystemData.SetSystem(new Engine.SystemData());
			SystemData.Current.Effects.LoadEffect("Burn", (definition, unit, source, originator, value) =>
			{
				unit.Properties.GetAs<IUnitCombatProperties>().CurrentHP -= 10;
				return null;
			});
		}

		[ClassCleanup]
		public static void Unload()
		{
			Engine.SystemData.SetSystem(data);
		}
		*/
		private IUnit MakeUnit()
			=> new Unit.Builder(StatsResolver.Default, LinkedStatsResolver.Default)
				.SetMainDetails("Test Unit", "Class")
				.SetBaseStats(Enum.GetValues(typeof(Attributes)), 10)
				.Build();

		private IEquipment MakeEquipment()
			=> new Equipment.Builder(EquipmentType.Armor)
				.SetName("Test Armor")
				.SetStatAddition(CombatStats.DEF, 3)
				.Build();
		
		[TestMethod]
		public void UnitSerialization()
		{
			IUnit unit = MakeUnit();
			IUnit clone = Serializer.DeepClone(unit);

			Assert.AreEqual(unit.Name, clone.Name);
			Assert.AreEqual(unit.Class, clone.Class);
			Assert.AreEqual(unit.Level, clone.Level);
			Assert.AreEqual(unit.Stats[Attributes.STR], clone.Stats[Attributes.STR]);
		}
		
		[TestMethod]
		public void EquipmentSerialization()
		{
			IEquipment armor = MakeEquipment();

			IEquipment clone = Serializer.DeepClone(armor);
			Assert.AreEqual(armor.Additions[CombatStats.DEF], clone.Additions[CombatStats.DEF]);
		}

		[TestMethod]
		public void WeaponSerialization()
		{
			IWeapon weapon = new Weapon.Builder(EquipmentType.Weapon, WeaponType.Sword)
				.AddDamageTypes(DamageType.Physical, Element.Fire)
				.Build();

			IWeapon clone = Serializer.DeepClone(weapon);
			
			Assert.AreEqual(weapon.DamageTypes.First(), clone.DamageTypes.First());
		}

		[TestMethod]
		public void BuffSerialization()
		{
			Buff buff = new Buff();
			buff.Multiplications[CombatStats.DEF] = 10;

			Buff clone = Serializer.DeepClone(buff);
			Assert.AreEqual(buff.Multiplications[CombatStats.DEF], clone.Multiplications[CombatStats.DEF]);
		}
		
		[TestMethod]
		public void EquippedUnitSerialization()
		{
			IUnit unit = MakeUnit();
			unit.ItemProperties.AddEquipmentSlot(EquipmentType.Armor);

			IEquipment armor = MakeEquipment();

			IUnit clone = (IUnit)Serializer.DeepClone(unit);

			Assert.AreEqual(unit.Stats[CombatStats.DEF], clone.Stats[CombatStats.DEF]);
		}
		/*
		[TestMethod]
		public void BuffEventSerialization()
		{
			Unit unit = MakeUnit();

			Buff burn = new Buff();
			burn.Name = "Burn";
			burn.IsDebuff = true;
			burn.UpkeepEffects.Add("Burn", 10);

			unit.ApplyBuff(burn);

			Unit clone = (Unit)Serializer.DeepClone(unit);

			unit.Initialize();
			clone.Initialize();
			unit.Upkeep();
			clone.Upkeep();

			Assert.AreEqual(unit.Modifiers.Count, clone.Modifiers.Count);
			Assert.AreEqual(unit.Stats[CombatStats.HP] - 10, unit.GetCombatProperties().CurrentHP);
			Assert.AreEqual(unit.GetCombatProperties().CurrentHP, clone.GetCombatProperties().CurrentHP);
		}

		[TestMethod]
		public void DeserializedModifierStackReferences()
		{
			Unit unit = MakeUnit();
			Unit clone = (Unit)Serializer.DeepClone(unit);

			Assert.IsNotNull(clone.Buffs.Target);

			Buff buff = new Buff();
			buff.Additions[CombatStats.DEF] = 10;

			clone.ApplyBuff(buff);
			Assert.AreEqual(unit.Stats[CombatStats.DEF] + 10, clone.Stats[CombatStats.DEF]);
		}

		[TestMethod]
		public void BattleUnitSerialization()
		{
			Battle battle = new Battle();

			IUnit unit = MakeUnit();

			battle.AddUnit(unit, 0);
			battle.Start();

			battle = (Battle)Serializer.DeepClone(battle);
			unit = battle.GetTeam(0)[0];

			Assert.AreEqual(unit.GetCombatProperties().CurrentHP, battle.CurrentUnit.GetCombatProperties().CurrentHP);
			battle.CurrentUnit.GetCombatProperties().CurrentHP -= 10;
			Assert.AreEqual(unit.GetCombatProperties().CurrentHP, battle.CurrentUnit.GetCombatProperties().CurrentHP);
		}
		*/
	}
}
