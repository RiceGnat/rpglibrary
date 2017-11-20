using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	public class UsableItem : Item, IUsableItem
	{
		public bool IsConsumable { get; set; }
		public int Remaining { get; set; }
		public UsableDuringState UsableDuring { get; set; }

		private EffectList effects = new EffectList();
		public IEffectList Effects { get { return effects; } }
		ICollection<KeyValuePair<string, int>> IEffectSource.Effects { get { return effects.ReadOnly; } }
		string IEffectSource.SourceName { get { return Name; } }

		string IAutoCatalogable.CatalogKey { get { return Name; } }

		public void Use()
		{
			if (IsConsumable) Remaining--;
		}
	}
}
