using System;

namespace Davfalcon.Revelator
{
	public class ModifierFactory
	{
		public IEquipment CreateEquipment(Func<Equipment, IEquipment> func)
		{
			Equipment equipment = new Equipment();

			equipment.AddStatModificationType(StatModType.Additive);
			equipment.AddStatModificationType(StatModType.Scaling);

			return func(equipment);
		}

		public IBuff CreateBuff(Func<Buff, IBuff> func)
		{
			Buff buff = new Buff();

			buff.AddStatModificationType(StatModType.Additive);
			buff.AddStatModificationType(StatModType.Scaling);

			return func(buff);
		}
	}
}
