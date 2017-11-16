using System.Collections.Generic;
using RPGLibrary;
using RPGLibrary.Items;
using Davfalcon.Engine.Combat;

namespace Davfalcon.Engine.Items
{
	public class UsableItem : Item, IUsableItem, IEffects
	{
		public int Remaining { get; set; }
		public UsableDuringState UsableDuring { get; set; }

		private EffectList effects = new EffectList();
		public IEffectList Effects { get { return effects; } }
		ICollection<KeyValuePair<string, int>> IEffects.Effects { get { return effects.ReadOnly; } }
		string IEffects.SourceName { get { return Name; } }

		public virtual IList<ILogEntry> Use(IUnit user, params object[] targets)
		{
			if (IsConsumable) Remaining--;
			List<ILogEntry> effects = new List<ILogEntry>();
			foreach (IUnit target in targets)
			{
				effects.AddRange(Data.Current.Effects.ApplyEffects(this, target, user));
			}
			return effects;
		}
	}
}
