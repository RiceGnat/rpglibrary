using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	public interface IBuff : ITimedModifier
	{
		string Source { get; set; }
		bool IsDebuff { get; }

		IList<ILogEntry> ApplyUpkeepEffects();
	}
}
