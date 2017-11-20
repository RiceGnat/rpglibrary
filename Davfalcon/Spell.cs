﻿using System;
using System.Collections.Generic;
using RPGLibrary.Collections.Generic;

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

		private ManagedList<IBuff> grantedBuffs = new ManagedList<IBuff>();
		public IList<IBuff> GrantedBuffs { get { return grantedBuffs; } }
		ICollection<IBuff> ISpell.GrantedBuffs { get { return grantedBuffs.ReadOnly; } }

		private EffectList effects = new EffectList();
		public IEffectList CastEffects { get { return effects; } }
		ICollection<KeyValuePair<string, int>> IEffectSource.Effects { get { return effects.ReadOnly; } }
		string IEffectSource.SourceName { get { return Name; } }

		string IAutoCatalogable.CatalogKey { get { return Name; } }
	}
}
