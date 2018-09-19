using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Weapon : Equipment, IWeapon
	{
		public int BaseDamage { get; set; }
		public int CritMultiplier { get; set; } = 1;
		public Enum Type { get; set; }
		public Enum AttackElement { get; set; }

		private EffectList effects = new EffectList();
		public IEffectList OnHitEffects { get { return effects; } }
		IEnumerable<IEffectArgs> IEffectSource.Effects { get { return effects.ReadOnly; } }
		string IEffectSource.SourceName { get { return Name; } }
	}
}
