using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator.Engine.Combat
{
	public interface ICombatStatBinding
	{
		Enum Hit { get; }
		Enum Dodge { get; }
		Enum Crit { get; }
		IEnumerable<Enum> VolatileStats { get; }

		Enum GetDamageScalingStat(Enum damageType);
		Enum GetDamageResistStat(Enum damageType);
	}
}
