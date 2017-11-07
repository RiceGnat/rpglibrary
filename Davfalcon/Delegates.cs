using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	public delegate void UnitEventHandler(IUnit unit);
	public delegate void SpellEventHandler(IUnit caster, ISpell spell, IUnit targets, IList<ILogEntry> effects);
	public delegate void BuffEventHandler(IUnit unit, IBuff buff, IList<ILogEntry> effects);
}
