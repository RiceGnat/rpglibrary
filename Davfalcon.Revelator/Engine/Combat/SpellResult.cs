using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator.Engine.Combat
{
	[Serializable]
	public class SpellResult : ILogEntry
	{
		public readonly string Caster;
		public readonly string Spell;
		public readonly string[] Targets;
		public readonly HitCheck[] Hit;
		public readonly Damage[] DamageDealt;
		public readonly StatChange[] HPLost;
		public readonly IList<ILogEntry>[] OtherEffects;

		public SpellResult(IUnit caster, ISpell spell, IUnit[] targets, HitCheck[] hit, Damage[] damage, StatChange[] hpLost, IList<ILogEntry>[] effects)
		{
			Caster = caster.Name;
			Spell = spell.Name;

			int n = targets.Length;
			Targets = new string[n];
			Hit = new HitCheck[n];
			DamageDealt = new Damage[n];
			HPLost = new StatChange[n];
			OtherEffects = new List<ILogEntry>[n];

			for (int i = 0; i < n; i++)
			{
				Targets[i] = targets[i].Name;
				Hit[i] = hit[i];
				DamageDealt[i] = damage[i];
				HPLost[i] = hpLost[i];
				OtherEffects[i] = new List<ILogEntry>(effects[i]);
			}
		}
	}
}
