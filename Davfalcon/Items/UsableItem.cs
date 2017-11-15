using System.Collections.Generic;
using RPGLibrary;
using RPGLibrary.Items;

namespace Davfalcon.Items
{
	public class UsableItem : Item, IUsableItem
	{
		public delegate void EffectHandler(IUnit user, IItem item, IList<ILogEntry> effects, params object[] targets);

		public int Remaining { get; set; }
		public UsableDuringState UsableDuring { get; set; }

		public event EffectHandler Effects;

		public virtual IList<ILogEntry> Use(IUnit user, params object[] targets)
		{
			if (IsConsumable) Remaining--;
			List<ILogEntry> effects = new List<ILogEntry>();
			Effects?.Invoke(user, this, effects, targets);
			return effects;
		}
	}
}
