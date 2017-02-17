using System;

namespace RPGLibrary.Stats
{
	/// <summary>
	/// Abstract base class for <see cref="IStats"/>.
	/// </summary>
	[Serializable]
	public abstract class StatsPrototype : IStats
	{
		// Allow implementation to decide how stat is returned
		public abstract int Get(string stat);

		public int Get(Enum stat)
		{
			// Translate enum to string before retrieving
			return Get(stat.ToString());
		}

		public int this[string stat]
		{
			get
			{
				return Get(stat);
			}
		}

		public int this[Enum stat]
		{
			get
			{
				return Get(stat);
			}
		}
	}
}
