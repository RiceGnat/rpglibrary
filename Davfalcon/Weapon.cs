using System;
using RPGLibrary.Items;

namespace Davfalcon
{
	[Serializable]
	public class Weapon : Equipment, IWeapon
	{
		public static Weapon Unarmed { get; private set; }

		public int BaseDamage { get; set; }
		public WeaponType Type { get; set; }
		public Element AttackElement { get; set; }

		public Weapon() : base(EquipmentSlot.Weapon) { }

		static Weapon()
		{
			Unarmed = new Weapon();
			Unarmed.Name = "Unarmed strike";
			Unarmed.BaseDamage = 0;
		}
	}
}
