using System.Collections.Generic;
using Davfalcon.Combat;
using RPGLibrary;

namespace Davfalcon.Items
{
	public class SpellItem : UsableItem
	{
		private ISpell spell;

		public override IList<ILogEntry> Use(IUnit user, params object[] targets)
		{
			IList<ILogEntry> effects = base.Use(user, targets);

			SpellCastOptions options = new SpellCastOptions
			{
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
