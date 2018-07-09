namespace Davfalcon
{
	public interface IUnit : RPGLibrary.IUnit
	{
		IUnitCombatProperties CombatProperties { get; }
		IUnitItemProperties ItemProperties { get; }
	}
}
