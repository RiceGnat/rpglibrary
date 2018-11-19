using System;

namespace Davfalcon.Revelator
{
	public interface IWeapon<T> : IEquipment<T>, IDamageSource, IEffectSource where T : IUnit
	{
		Enum WeaponType { get; }
	}

	public interface IWeapon : IWeapon<IUnit> { }
}
