using Davfalcon.Collections.Generic;

namespace Davfalcon.Revelator.Engine
{
	public class WeaponRegistry : SelfRegisteredPrototypeCloner<IWeapon>, IWeaponRegistry
	{
		public IWeapon GetWithEffects(string weaponName, EffectArgs[] effects)
		{
			IWeapon weapon = Get(weaponName);
			foreach (EffectArgs effect in effects)
			{
				//weapon.OnHitEffects.Add(effect);
			}
			return weapon;
		}
	}
}
