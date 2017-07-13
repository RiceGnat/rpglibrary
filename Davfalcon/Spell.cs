using System;
using  RPGLibrary;
using Davfalcon.Combat;

namespace Davfalcon
{
	public enum SpellTargetType
	{
		Self, Target, Area, Line, Attack
	}

	public delegate void SpellEventHandler(IUnit caster, ISpell spell, params IUnit[] targets);

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

		public event SpellEventHandler OnCast;
	}
}
