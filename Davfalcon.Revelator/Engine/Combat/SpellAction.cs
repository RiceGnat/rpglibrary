using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator.Engine.Combat
{
	[Serializable]
	public class SpellAction : ILogEntry
	{
		public readonly string Caster;
		public readonly string Spell;
		public readonly string[] Targets;
		public readonly HitCheck[] Hit;
		public readonly Damage[] DamageDealt;
		public readonly PointLoss[] HPLost;
		public readonly IList<ILogEntry>[] OtherEffects;

		public SpellAction(IUnit caster, ISpell spell, IUnit[] targets, HitCheck[] hit, Damage[] damage, PointLoss[] hpLost, IList<ILogEntry>[] effects)
		{
			Caster = caster.Name;
			Spell = spell.Name;

			int n = targets.Length;
			Targets = new string[n];
			Hit = new HitCheck[n];
			DamageDealt = new Damage[n];
			HPLost = new PointLoss[n];
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

		public override string ToString()
		{
			string s = String.Format("{0} casts {1}.", Caster, Spell);

			for (int i = 0; i < Targets.Length; i++)
			{
				if (Hit[i].IsSet && !Hit[i].Hit)
					s += Environment.NewLine + String.Format("The spell misses {0}.", Targets[i]);

				if (Hit[i].Crit)
					s += Environment.NewLine + String.Format("The spell crits {0}!", Targets[i]);

				if (DamageDealt[i] != null)
					s += Environment.NewLine + DamageDealt[i].LogWith(HPLost[i]);

				foreach (ILogEntry effect in OtherEffects[i])
					s += Environment.NewLine + effect;
			}

			return s;
		}
	}
}
