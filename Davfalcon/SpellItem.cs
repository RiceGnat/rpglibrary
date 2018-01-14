using System;

namespace Davfalcon
{
	[Serializable]
	public class SpellItem : UsableItem, ISpellItem
	{
		public ISpell Spell { get; set; }
	}
}
