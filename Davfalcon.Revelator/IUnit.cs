using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IUnit : Davfalcon.IUnit
	{
		IDictionary<Enum, int> VolatileStats { get; }
		new IModifierCollection<IUnit> Modifiers { get; }
		IUnitEquipmentManager<IUnit> Equipment { get; }
		IModifierCollection<IUnit> Buffs { get; }
	}
}
