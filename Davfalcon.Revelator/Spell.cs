using System;
using System.Collections.Generic;
using Davfalcon.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Spell : ISpell, IEditableDescription
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public Enum TargetType { get; set; }
		public int Cost { get; set; }
		public int BaseDamage { get; set; }
		public Enum BonusDamageStat { get; set; }
		public int CritMultiplier { get; set; }
		public IEnumerable<Enum> DamageTypes { get; set; }
		public int BaseHeal { get; set; }
		public int Range { get; set; }
		public int Size { get; set; }
		public int MaxTargets { get; set; }

		private ManagedList<IBuff> grantedBuffs = new ManagedList<IBuff>();
		public IList<IBuff> GrantedBuffs => grantedBuffs;
		ICollection<IBuff> ISpell.GrantedBuffs => grantedBuffs.ReadOnly;

		private EffectList effects = new EffectList();
		public IEffectList CastEffects => effects;
		IEnumerable<IEffectArgs> IEffectSource.Effects => effects.ReadOnly;
		string IEffectSource.SourceName => Name;
	}
}
