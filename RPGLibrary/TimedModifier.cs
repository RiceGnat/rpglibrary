using System;

namespace RPGLibrary
{
	/// <summary>
	/// Implements a modifier with a timer.
	/// </summary>
	[Serializable]
	public class TimedModifier : UnitStatsModifier, ITimedModifier
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public int Duration { get; set; }
		public int Remaining { get; set; }

		public virtual void Reset()
		{
			Remaining = Duration;
		}

		public virtual void Tick()
		{
			if (Remaining > 0) Remaining--;
		}

		// Resolve Name property ambiguity
		string IUnit.Name { get { return InterfaceUnit.Name; } }
		string ITimedModifier.Name { get { return Name; } }
	}
}
