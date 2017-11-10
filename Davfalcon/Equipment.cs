using System;
using System.Collections.Generic;

namespace Davfalcon
{
	[Serializable]
	public class Equipment : RPGLibrary.Items.Equipment, IEquipment
	{
		public EquipmentSlot Slot { get; protected set; }

		private readonly List<IBuff> grantedEffects;
		public IList<IBuff> GrantedEffects { get; private set; }

		protected Equipment()
			: base()
		{
			grantedEffects = new List<IBuff>();
			GrantedEffects = grantedEffects.AsReadOnly();
		}

		public Equipment(EquipmentSlot slot)
			: this()
		{
			Slot = slot;
		}
	}
}
