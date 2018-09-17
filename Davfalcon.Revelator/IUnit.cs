namespace Davfalcon.Revelator
{
	public interface IUnit : Davfalcon.IUnit
	{
		IUnitCombatProperties CombatProperties { get; }
		IUnitItemProperties ItemProperties { get; }
	}
}
