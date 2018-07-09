using RPGLibrary.Collections.Generic;

namespace Davfalcon.Engine
{
	public interface IWeaponRegistry : ISelfRegistry<IWeapon>
	{
		IWeapon GetWithEffects(string weaponName, params IEffectArgs[] effects);
	}
}
