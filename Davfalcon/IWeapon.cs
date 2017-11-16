using RPGLibrary.Items;

namespace Davfalcon
{
	public interface IWeapon : IEquipment
	{
		int BaseDamage { get; }
		int CritMultiplier { get; }
		WeaponType Type { get; }
		Element AttackElement { get; }
	}
}
