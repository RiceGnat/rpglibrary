using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Weapon : Equipment, IWeapon
	{
		public Enum WeaponType { get; set; }
		public int BaseDamage { get; set; }
		public Enum BonusDamageStat { get; set; }
		public int CritMultiplier { get; set; } = 1;
		public IList<Enum> DamageTypes { get; set; } = new List<Enum>();
		IEnumerable<Enum> IDamageSource.DamageTypes => DamageTypes;

		private EffectList effects = new EffectList();
		public IEffectList OnHitEffects => effects;
		IEnumerable<IEffectArgs> IEffectSource.Effects => effects.ReadOnly;
		string IEffectSource.SourceName => Name;
	}
}
