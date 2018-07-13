using System;
using System.Collections.Generic;
using Saffron;

namespace Davfalcon
{
	[Serializable]
	public class UsableItem : Item, IUsableItem, IDescribable
	{
		public bool IsConsumable { get; set; }
		public int Remaining { get; set; }
		public UsableDuringState UsableDuring { get; set; }

		private EffectList effects = new EffectList();
		public IEffectList Effects { get { return effects; } }
		IEnumerable<IEffectArgs> IEffectSource.Effects { get { return effects.ReadOnly; } }
		string IEffectSource.SourceName { get { return Name; } }

		public void Use()
		{
			if (IsConsumable) Remaining--;
		}
	}
}
