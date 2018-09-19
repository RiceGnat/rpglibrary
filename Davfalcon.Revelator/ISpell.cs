using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface ISpell : IEffectSource, IDescribable
	{
		Enum TargetType { get; }
		Enum SpellElement { get; }
		Enum DamageType { get; }
		int Cost { get; }
		int BaseDamage { get; }
		int BaseHeal { get; }
		int Range { get; }
		int Size { get; }
		int MaxTargets { get; }
		ICollection<IBuff> GrantedBuffs { get; }
	}
}
