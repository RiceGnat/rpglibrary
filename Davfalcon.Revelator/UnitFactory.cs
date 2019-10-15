using Davfalcon.Buffs;

namespace Davfalcon.Revelator
{
	public sealed class UnitFactory
	{
		public IUnit CreateCharacterUnit(string name) => Unit.Create(unit =>
		{
			unit.Name = name;
			unit.AddComponent(UnitComponents.Equipment, new UnitEquipmentManager());
			unit.AddComponent(UnitComponents.Buffs, new UnitBuffManager<IUnit, IBuff>());

			unit.BaseStats[Attributes.LVL] = 1;
			unit.BaseStats[Attributes.STR] = 10;
			unit.BaseStats[Attributes.VIT] = 10;
			unit.BaseStats[Attributes.INT] = 10;
			unit.BaseStats[Attributes.WIS] = 10;
			unit.BaseStats[Attributes.AGI] = 10;
			unit.BaseStats[Attributes.LUK] = 10;

			unit.StatDerivations[CombatStats.ATK] = stats => stats[Attributes.STR] * 2;
			unit.StatDerivations[CombatStats.DEF] = stats => stats[Attributes.STR] + stats[Attributes.VIT];
			unit.StatDerivations[CombatStats.MAG] = stats => stats[Attributes.INT] + stats[Attributes.WIS];
			unit.StatDerivations[CombatStats.RES] = stats => stats[Attributes.WIS] * 2;
			unit.StatDerivations[CombatStats.HIT] = stats => stats[Attributes.AGI];
			unit.StatDerivations[CombatStats.AVD] = stats => stats[Attributes.AGI] + stats[Attributes.LUK];
			unit.StatDerivations[CombatStats.HP] = stats => stats[Attributes.VIT] * 5;
			unit.StatDerivations[CombatStats.MP] = stats => stats[Attributes.INT] * 2 + stats[Attributes.WIS] * 2;

			unit.GetEquipmentManager().AddEquipmentSlot(EquipmentType.Weapon);
			unit.GetEquipmentManager().AddEquipmentSlot(EquipmentType.Armor);
			unit.GetEquipmentManager().AddEquipmentSlot(EquipmentType.Accessory);
			unit.GetEquipmentManager().AddEquipmentSlot(EquipmentType.Accessory);

			return unit;
		});

		public IUnit CreateSimpleUnit(string name) => Unit.Create(unit =>
		{
			unit.Name = name;
			unit.AddComponent(UnitComponents.Buffs, new UnitBuffManager<IUnit, IBuff>());

			unit.BaseStats[Attributes.LVL] = 1;

			unit.StatDerivations[CombatStats.ATK] = stats => 10 + stats[Attributes.LVL] * 2;
			unit.StatDerivations[CombatStats.DEF] = stats => 10 + stats[Attributes.LVL];
			unit.StatDerivations[CombatStats.MAG] = stats => 5 + stats[Attributes.LVL];
			unit.StatDerivations[CombatStats.RES] = stats => 5 + stats[Attributes.LVL];
			unit.StatDerivations[CombatStats.HIT] = stats => 10 + stats[Attributes.LVL] / 2;
			unit.StatDerivations[CombatStats.AVD] = stats => 10 + stats[Attributes.LVL] / 2;
			unit.StatDerivations[CombatStats.HP] = stats => 20 + stats[Attributes.LVL] * 5;
			unit.StatDerivations[CombatStats.MP] = stats => 10 + stats[Attributes.LVL] * 3;

			return unit;
		});
	}
}
