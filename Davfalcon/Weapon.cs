using RPGLibrary.Items;

namespace Davfalcon
{
	public enum WeaponType
	{
		Sword, Dagger, Axe, Spear, Bow, Staff
	}
	public class Weapon : Equipment, IWeapon
	{
		public int BaseDamage { get; set; }
		public WeaponType Type { get; set; }
		public Element AttackElement { get; set; }

		public Weapon() : base(EquipmentSlot.Weapon) { }
	}
}
