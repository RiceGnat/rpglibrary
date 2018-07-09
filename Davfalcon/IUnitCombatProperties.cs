using RPGLibrary;
using System.Collections.Generic;

namespace Davfalcon
{
	public interface IUnitCombatProperties
	{
		int CurrentHP { get; set; }
		int CurrentMP { get; set; }
		IUnitModifierStack Buffs { get; }
		IUnitBattleState BattleState { get; set; }

		IWeapon GetEquippedWeapon();
	}
}
