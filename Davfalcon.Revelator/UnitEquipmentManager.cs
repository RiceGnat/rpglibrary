using Davfalcon.Equipment;

namespace Davfalcon.Revelator
{
	public class UnitEquipmentManager : UnitEquipmentManager<IUnit, EquipmentType, IEquipment>, IUnitComponent<IUnit>
	{
		private IUnit owner;

		protected override void SetSlotEquipment(EquipmentSlot slot, IEquipment equipment)
		{
			base.SetSlotEquipment(slot, equipment);
			foreach (IBuff buff in equipment.GetBuffs())
			{
				buff.Owner = owner;
			}
		}

		public override void Initialize(IUnit unit)
		{
			base.Initialize(unit);
			owner = unit;
		}
	}
}
