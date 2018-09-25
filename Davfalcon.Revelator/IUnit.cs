namespace Davfalcon.Revelator
{
	public interface IUnit : Davfalcon.IUnit
	{
		IUnitEquipmentManager Equipment { get; }
		IUnitCombatProperties CombatProperties { get; }
		IUnitItemProperties ItemProperties { get; }
	}
}
