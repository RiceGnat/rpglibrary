using System;
using System.Collections.Generic;
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

		protected override int Resolve(int baseValue, IReadOnlyDictionary<Enum, int> modifications) => StatsFunctions.Resolve(baseValue, modifications);

		protected override Func<int, int, int> GetAggregator(Enum modificationType) => StatsFunctions.GetAggregator(modificationType);

		protected override int GetAggregatorSeed(Enum modificationType) => StatsFunctions.GetAggregatorSeed(modificationType);
	}
}
