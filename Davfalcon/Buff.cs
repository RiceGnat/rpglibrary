using System;
using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	[Serializable]
	public class Buff : TimedModifier, IBuff
	{
		public string Source { get; set; }
		public bool IsDebuff { get; set; }

		public event BuffEventHandler UpkeepEffects;

		public IList<ILogEntry> ApplyUpkeepEffects()
		{
			IList<ILogEntry> effects = new List<ILogEntry>();

			UpkeepEffects?.Invoke(Target.Modifiers, this, effects);

			return effects;
		}
	}
}
