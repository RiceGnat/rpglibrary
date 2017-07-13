using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	public interface IBuff : ITimedModifier
	{
		string Source { get; set; }

		event BuffEventHandler UpkeepEffects;

		IList<ILogEntry> ApplyUpkeepEffects();
	}
}
