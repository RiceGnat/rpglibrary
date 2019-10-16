namespace Davfalcon.Revelator
{
	public interface IWeapon : IEquipment, IEffectSource
	{
		int BaseDamage { get; }
		int CritMultiplier { get; }
		WeaponType Type { get; }
		Element AttackElement { get; }
		IEffectList OnHitEffects { get; }
	}
}
