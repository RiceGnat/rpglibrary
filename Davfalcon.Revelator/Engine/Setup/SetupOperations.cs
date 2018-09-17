using RPGLibrary;

namespace Davfalcon.Engine.Setup
{
	public static class SetupOperations
	{
		public static void AddBuff(this Equipment equipment, string buffName)
			=> equipment.GrantedBuffs.Add(SystemData.Current.Buffs.Get(buffName));

		public static void AddBuff(this Spell spell, string buffName)
			=> spell.GrantedBuffs.Add(SystemData.Current.Buffs.Get(buffName));

		public static void Equip(this IUnit unit, string equipmentName)
		{
			IEquipment equipment = SystemData.Current.Equipment.Get(equipmentName);
			unit.Properties.GetAs<IUnitItemProperties>().EquipmentLookup.Add(equipment.Slot, equipment);
		}
	}
}
