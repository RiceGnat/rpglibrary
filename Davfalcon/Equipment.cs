﻿using System;
using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	[Serializable]
	public class Equipment : ModifierItem, IEquipment
	{
		public EquipmentSlot Slot { get; set; }

		private readonly List<IBuff> grantedBuffs = new List<IBuff>();
		public IList<IBuff> GrantedBuffs { get { return grantedBuffs; } }
		private readonly IList<IBuff> grantedBuffsReadOnly;
		IList<IBuff> IEquipment.GrantedBuffs { get { return grantedBuffsReadOnly; } }

		protected Equipment()
			: base()
		{
			grantedBuffsReadOnly = grantedBuffs.AsReadOnly();
		}

		public Equipment(EquipmentSlot slot)
			: this()
		{
			Slot = slot;
		}
	}
}
