using Davfalcon.Buffs;

namespace Davfalcon.Revelator
{
	public class UnitFactory
	{
		public IUnit CreateUnit(string name) => Unit.Create(unit =>
		{
			unit.Name = name;
			unit.AddComponent(UnitComponents.Equipment, new UnitEquipmentManager());
			unit.AddComponent(UnitComponents.Buffs, new UnitBuffManager<IUnit, IBuff>());
			return unit;
		});
	}
}
