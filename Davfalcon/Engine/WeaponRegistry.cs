using RPGLibrary.Collections.Generic;

namespace Davfalcon.Engine
{
	public class WeaponRegistry : SelfRegisteredPrototypeCloner<IWeapon>, IWeaponRegistry
	{
		public IWeapon GetWithEffects(string weaponName, IEffectArgs[] effects)
		{
			IWeapon weapon = Get(weaponName);
			foreach (IEffectArgs effect in effects)
			{
				weapon.OnHitEffects.Add(effect);
			}
			return weapon;
		}
	}
}
