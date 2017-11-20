using System.Collections.Generic;
using RPGLibrary;
using RPGLibrary.Items;

namespace Davfalcon.Engine.Items
{
	public class UsableItem : Item, IUsableItem, IEffectSource
	{
		public bool IsConsumable { get; set; }
		public int Remaining { get; set; }
		public UsableDuringState UsableDuring { get; set; }

		private EffectList effects = new EffectList();
		public IEffectList Effects { get { return effects; } }
		ICollection<KeyValuePair<string, int>> IEffectSource.Effects { get { return effects.ReadOnly; } }
		string IEffectSource.SourceName { get { return Name; } }

		public virtual IList<ILogEntry> Use(IUnit user, params object[] targets)
		{
			if (IsConsumable) Remaining--;
			List<ILogEntry> effects = new List<ILogEntry>();
			foreach (IUnit target in targets)
			{
				effects.AddRange(System.Current.Effects.ApplyEffects(this, target, user));
			}
			return effects;
		}
	}
}
