using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IUnitCombatProperties
	{
		int CurrentHP { get; set; }
		int CurrentMP { get; set; }
		IUnitModifierStack Buffs { get; }
		IUnitBattleState BattleState { get; set; }
	}
}
