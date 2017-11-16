using System;
using System.Collections.Generic;
using RPGLibrary.Items;

namespace Davfalcon
{
	[Serializable]
	public class Equipment : ModifierItem, IEquipment
	{
		public EquipmentSlot Slot { get; protected set; }

		private readonly List<IBuff> grantedEffects = new List<IBuff>();
		public IList<IBuff> GrantedEffects { get { return grantedEffects; } }
		private readonly IList<IBuff> grantedEffectsReadOnly;
		IList<IBuff> IEquipment.GrantedEffects { get { return grantedEffectsReadOnly; } }

		protected Equipment()
			: base()
		{
			grantedEffectsReadOnly = grantedEffects.AsReadOnly();
		}

		public Equipment(EquipmentSlot slot)
			: this()
		{
			Slot = slot;
		}
	}
}
