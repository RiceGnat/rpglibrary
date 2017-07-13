using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	public interface ISpell
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
		IList<IBuff> GrantedBuffs { get; }

		event SpellEventHandler CastEffects;

		IList<ILogEntry> ApplyCastEffects(IUnit caster, IUnit targets);
	}
}
