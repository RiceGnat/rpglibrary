using RPGLibrary;
using System.Collections.Generic;

namespace Davfalcon
{
	public interface IUnitCombatProperties
	{
		int CurrentHP { get; set; }
		int CurrentMP { get; set; }
		IWeapon EquippedWeapon { get; }
		IEnumerable<IEquipment> Equipment { get; }
		IUnitModifierStack Buffs { get; }
		IUnitBattleState BattleState { get; set; }
	}
}
