using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IWeapon : IEquipment, IEffectSource
	{
		int BaseDamage { get; }
		int CritMultiplier { get; }
		Enum WeaponType { get; }
		IEnumerable<Enum> DamageTypes { get; }
		IEffectList OnHitEffects { get; }
	}
}
