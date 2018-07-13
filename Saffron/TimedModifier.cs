﻿using System;

namespace Saffron
{
	/// <summary>
	/// Implements a modifier with a timer.
	/// </summary>
	[Serializable]
	public class TimedModifier : UnitStatsModifier, ITimedModifier
	{
		/// <summary>
		/// Gets or sets the maximum duration for the modifier.
		/// </summary>
		public int Duration { get; set; }

		/// <summary>
		/// Gets or sets the remaining time for the modifier.
		/// </summary>
		public int Remaining { get; set; }

		/// <summary>
		/// Resets the remaining time to the duration.
		/// </summary>
		public virtual void Reset()
		{
			Remaining = Duration;
		}

		/// <summary>
		/// Decrements the remaining time if it is greater than 0.
		/// </summary>
		public virtual void Tick()
		{
			if (Remaining > 0) Remaining--;
		}
	}
}
