using RPGLibrary;
using Davfalcon.UnitManagement;

namespace Davfalcon.Engine
{
	public static class SetupOperations
	{
		public static void AddBuff(this Equipment equipment, string buffName)
			=> equipment.GrantedBuffs.Add(SystemData.Current.Buffs.Get(buffName));

		public static void AddBuff(this Spell spell, string buffName)
			=> spell.GrantedBuffs.Add(SystemData.Current.Buffs.Get(buffName));

		public static void Equip(this IUnit unit, string equipmentName)
			=> unit.Equip(SystemData.Current.Equipment.Get(equipmentName));
	}
}
