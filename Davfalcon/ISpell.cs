using System.Collections.Generic;

namespace Davfalcon
{
	public interface ISpell : IEffectSource, IAutoCatalogable
	{
		string Name { get; }
		string Description { get; }

		SpellTargetType TargetType { get; }
		Element SpellElement { get; }
		DamageType DamageType { get; }
		int Cost { get; }
		int BaseDamage { get; }
		int BaseHeal { get; }
		int Range { get; }
		int Size { get; }
		int MaxTargets { get; }
		ICollection<IBuff> GrantedBuffs { get; }
	}
}
