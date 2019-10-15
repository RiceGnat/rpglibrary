using System;

namespace Davfalcon.Revelator
{
	class Program
	{
		private static readonly UnitFactory unitFactory = new UnitFactory();
		private static readonly ModifierFactory modifierFactory = new ModifierFactory();

		static void Main(string[] args)
		{
			IUnit unit = unitFactory.CreateCharacterUnit("Davfalcon");

			IEquipment weapon = modifierFactory.CreateEquipment(equipment =>
			{
				equipment.Name = "Trusty Halberd";
				equipment.EquipmentType = EquipmentType.Weapon;

				equipment.StatModifications[StatModType.Additive][CombatStats.ATK] = 10;
				equipment.StatModifications[StatModType.Additive][CombatStats.HIT] = 5;

				return equipment;
			});

			IEquipment armor = modifierFactory.CreateEquipment(equipment =>
			{
				equipment.Name = "Enchanted Mail";
				equipment.EquipmentType = EquipmentType.Armor;

				equipment.StatModifications[StatModType.Additive][CombatStats.DEF] = 10;

				equipment.AddBuff(modifierFactory.CreateBuff(buff =>
				{
					buff.Name = "Vitality";

					buff.StatModifications[StatModType.Scaling][Attributes.VIT] = 20;

					return buff;
				}));

				return equipment;
			});

			unit.GetEquipmentManager().Equip(weapon);
			unit.GetEquipmentManager().Equip(armor);

			IUnit modified = unit.Modifiers.AsModified();

			Console.Write(unit.Modifiers.AsModified().PrettyPrint());

			Console.ReadKey();
		}
	}
}
