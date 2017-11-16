using System;

namespace RPGLibrary
{
	/// <summary>
	/// Implements a modifier with a timer.
	/// </summary>
	[Serializable]
	public class TimedModifier : UnitStatsModifier, ITimedModifier
	{
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
	}
}
