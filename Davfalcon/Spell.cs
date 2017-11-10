using System;
using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	[Serializable]
	public class Spell : ISpell
	{
		public delegate void EventHandler(IUnit caster, ISpell spell, IUnit targets, IList<ILogEntry> effects);

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

		private readonly List<IBuff> grantedBuffs;
		public IList<IBuff> GrantedBuffs { get; private set; }

		public event EventHandler CastEffects;

		public IList<ILogEntry> ApplyCastEffects(IUnit caster, IUnit targets)
		{
			IList<ILogEntry> effects = new List<ILogEntry>();

			CastEffects?.Invoke(caster, this, targets, effects);

			return effects;
		}

		public Spell()
		{
			grantedBuffs = new List<IBuff>();
			GrantedBuffs = grantedBuffs.AsReadOnly();
		}
	}
}
