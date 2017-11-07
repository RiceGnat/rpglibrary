using System;
using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	[Serializable]
	public class Spell : ISpell
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public SpellTargetType TargetType { get; set; }
		public Element SpellElement { get; set; }
		public DamageType DamageType { get; set; }
		public int Cost { get; set; }
		public int BaseDamage { get; set; }
		public int BaseHeal { get; set; }
		public int Range { get; set; }
		public int Size { get; set; }
		public int MaxTargets { get; set; }
		public IList<IBuff> GrantedBuffs { get; protected set; }

		public event SpellEventHandler CastEffects;

		public IList<ILogEntry> ApplyCastEffects(IUnit caster, IUnit targets)
		{
			IList<ILogEntry> effects = new List<ILogEntry>();

			CastEffects?.Invoke(caster, this, targets, effects);

			return effects;
		}

		public Spell()
		{
			GrantedBuffs = new List<IBuff>();
		}
	}
}
