using RPGLibrary.Items;

namespace Davfalcon
{
	public enum WeaponType
	{
		Sword, Dagger, Axe, Spear, Bow, Staff
	}
	public class Weapon : Equipment
	{
		public int BaseDamage { get; set; }
		public WeaponType Type { get; set; }
		public Element AttackElement { get; set; }

		public Weapon() : base()
		{
			Slot = EquipmentSlot.Weapon;
		}
	}
}
