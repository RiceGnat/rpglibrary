using System;
using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon.Combat
{
	[Serializable]
	public class SpellAction : ILogEntry
	{
		public readonly string Caster;
		public readonly string Spell;
		public readonly string[] Targets;
		public readonly Damage[] DamageDealt;
		public readonly HPLoss[] HPLost;
		public readonly IList<ILogEntry>[] OtherEffects;

		public SpellAction(IUnit caster, ISpell spell, IUnit[] targets, Damage[] damage, HPLoss[] hpLost, IList<ILogEntry>[] effects)
		{
			Caster = caster.Name;
			Spell = spell.Name;

			int n = targets.Length;
			Targets = new string[n];
			DamageDealt = new Damage[n];
			HPLost = new HPLoss[n];
			OtherEffects = new List<ILogEntry>[n];

			for (int i = 0; i < n; i++)
			{
				Targets[i] = targets[i].Name;
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
				if (DamageDealt[i] != null)
					s += Environment.NewLine +
						 DamageDealt[i] + Environment.NewLine +
						 HPLost[i];

				foreach (ILogEntry effect in OtherEffects[i])
					s += Environment.NewLine +
						 effect;
			}

			return s;
		}
	}
}
