using System;
using System.Collections.Generic;
using Saffron;

namespace Davfalcon
{
	[Serializable]
	public class Equipment : ModifierItem, IEquipment
	{
		public EquipmentType SlotType { get; set; }

		private readonly List<IBuff> grantedBuffs = new List<IBuff>();
		public IList<IBuff> GrantedBuffs { get { return grantedBuffs; } }
		private readonly IList<IBuff> grantedBuffsReadOnly;
		IList<IBuff> IEquipment.GrantedBuffs { get { return grantedBuffsReadOnly; } }

		public Equipment()
			: base()
		{
			grantedBuffsReadOnly = grantedBuffs.AsReadOnly();
		}

		public Equipment(EquipmentType slot) : this()
		{
			SlotType = slot;
		}
	}
}
