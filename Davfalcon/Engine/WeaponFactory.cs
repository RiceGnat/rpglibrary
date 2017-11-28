namespace Davfalcon.Engine
{
	internal class WeaponFactory : Catalog<IWeapon>, IWeaponFactory
	{
		public IWeapon Get(string weaponName, IEffectArgs[] effects)
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
