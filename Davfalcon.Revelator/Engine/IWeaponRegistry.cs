using Davfalcon.Collections.Generic;

namespace Davfalcon.Revelator.Engine
{
	public interface IWeaponRegistry : ISelfRegistry<IWeapon>
	{
		IWeapon GetWithEffects(string weaponName, params EffectArgs[] effects);
	}
}
