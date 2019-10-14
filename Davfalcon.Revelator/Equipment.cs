using System;
using System.Linq;
using Davfalcon.Equipment;

namespace Davfalcon.Revelator
{
	[Serializable]
	public sealed class Equipment : CompoundEquipment<IUnit, EquipmentType>, IEquipment, IUnit
	{
		protected override IUnit SelfAsUnit => this;

		public void AddBuff(IBuff buff)
		{
			buff.Source = Name;
			Modifiers.Add(buff);
		}

		public IBuff[] GetBuffs() => Modifiers.Cast<IBuff>().ToArray();
	}
}
