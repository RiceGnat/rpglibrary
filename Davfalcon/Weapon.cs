using System;
using System.Collections.Generic;

namespace Davfalcon
{
	[Serializable]
	public class Weapon : Equipment, IWeapon
	{
		public static Weapon Unarmed { get; private set; }

		public int BaseDamage { get; set; }
		public int CritMultiplier { get; set; } = 1;
		public WeaponType Type { get; set; }
		public Element AttackElement { get; set; }

		private EffectList effects = new EffectList();
		public IEffectList OnHitEffects { get { return effects; } }
		IEnumerable<IEffectArgs> IEffectSource.Effects { get { return effects.ReadOnly; } }
		string IEffectSource.SourceName { get { return Name; } }

		public Weapon() : base(EquipmentType.Weapon) { }

		static Weapon()
		{
			Unarmed = new Weapon();
			Unarmed.Name = "Unarmed strike";
			Unarmed.BaseDamage = 0;
			Unarmed.Type = WeaponType.Fist;
		}
	}
}
