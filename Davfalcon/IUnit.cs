namespace Davfalcon
{
	public interface IUnit : Saffron.IUnit
	{
		IUnitCombatProperties CombatProperties { get; }
		IUnitItemProperties ItemProperties { get; }
	}
}
