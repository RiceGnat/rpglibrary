using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IUnitCombatProperties
	{
		IDictionary<Enum, int> VolatileStats { get; }
		IUnitModifierStack Buffs { get; }
		IUnitBattleState BattleState { get; set; }
	}
}
