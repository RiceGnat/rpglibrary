using System.Collections.Generic;
using Davfalcon.Engine.Combat;
using RPGLibrary;
using RPGLibrary.Items;

namespace Davfalcon.Engine.Items
{
	public class SpellItem : Item, IUsableItem
	{
		public int Remaining { get; set; }
		public UsableDuringState UsableDuring { get; set; }

		private ISpell spell;

		public IList<ILogEntry> Use(IUnit user, params object[] targets)
		{
			List<ILogEntry> effects = new List<ILogEntry>();

			SpellCastOptions options = new SpellCastOptions
			{
				AdjustedCost = 0,
				NoScaling = true
			};

			effects.Add(user.Cast(spell, options, (IUnit[])targets));

			return effects;
		}

		public SpellItem(ISpell spell)
		{
			this.spell = spell;
		}
	}
}
