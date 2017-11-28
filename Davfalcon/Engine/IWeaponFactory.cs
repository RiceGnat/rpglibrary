namespace Davfalcon.Engine
{
	public interface IWeaponFactory : IAutoCatalog<IWeapon>
	{
		IWeapon Get(string weaponName, params IEffectArgs[] effects);
	}
}
