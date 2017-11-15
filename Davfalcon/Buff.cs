using System;
using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	[Serializable]
	public class Buff : TimedModifier, IBuff
	{
		public delegate void EffectHandler(IUnit unit, IBuff buff, IList<ILogEntry> effects);

		public string Source { get; set; }
		public bool IsDebuff { get; set; }

		public event EffectHandler UpkeepEffects;

		public IList<ILogEntry> ApplyUpkeepEffects()
		{
			List<ILogEntry> effects = new List<ILogEntry>();
			UpkeepEffects?.Invoke(Target.Modifiers, this, effects);
			return effects;
		}
	}
}
