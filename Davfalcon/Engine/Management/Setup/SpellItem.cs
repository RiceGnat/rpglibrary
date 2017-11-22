using System;

namespace Davfalcon.Engine.Management.Setup
{
	[Serializable]
	public class SpellItem : UsableItem, ISpellItem
	{
		public ISpell Spell { get; private set; }

		public SpellItem(ISpell spell)
		{
			Spell = spell;
		}
	}
}
