using System;
using System.Collections.Generic;

namespace Davfalcon
{
	public interface IDamageSource
	{
		int BaseDamage { get; }
		int CritMultiplier { get; }
		Enum BonusDamageStat { get; }
		IEnumerable<Enum> DamageTypes { get; }
	}
}
