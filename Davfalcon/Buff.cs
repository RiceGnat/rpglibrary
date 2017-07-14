using System;
using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	public delegate void BuffEventHandler(IUnit unit, IBuff buff, IList<ILogEntry> effects);

	[Serializable]
	public class Buff : TimedModifier, IBuff
	{
		public string Source { get; set; }
		public bool IsDebuff { get; set; }

		public event BuffEventHandler UpkeepEffects;

		public IList<ILogEntry> ApplyUpkeepEffects()
		{
			IList<ILogEntry> effects = new List<ILogEntry>();

			if (UpkeepEffects != null)
			{
				UpkeepEffects(Target.Modifiers, this, effects);
			}

			return effects;
		}
	}
}
