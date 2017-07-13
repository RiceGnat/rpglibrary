using System;
using System.Collections.Generic;

namespace Davfalcon
{
	public enum EquipmentSlot
	{
		Weapon, Armor, Accessory
	}

	[Serializable]
	public class Equipment : RPGLibrary.Items.Equipment, IEquipment
	{
		public EquipmentSlot Slot { get; protected set; }

		public IList<IBuff> GrantedEffects { get; protected set; }

		protected Equipment()
			: base()
		{
			GrantedEffects = new List<IBuff>();
		}

		public Equipment(EquipmentSlot slot)
			: this()
		{
			Slot = slot;
		}
	}
}
