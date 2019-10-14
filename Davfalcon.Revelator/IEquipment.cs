using Davfalcon.Equipment;

namespace Davfalcon.Revelator
{
	public interface IEquipment : IEquipment<IUnit, EquipmentType>
	{
		void AddBuff(IBuff buff);

		IBuff[] GetBuffs();
	}
}
