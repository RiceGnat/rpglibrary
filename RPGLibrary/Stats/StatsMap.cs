using System;
using System.Collections.Generic;

namespace RPGLibrary.Stats
{
	/// <summary>
	/// Implements data structure for stat lookup.
	/// </summary>
	[Serializable]
	public class StatsMap : StatsPrototype, IStatsEditable
	{
		private Dictionary<string, int> map = new Dictionary<string, int>();

		public override int Get(string stat)
		{
			// Still unsure whether this should simply return 0 or an exception
			// On one hand, checking for existence would be better for the caller than having to catch an exception
			// But there shouldn't be a reason to look up a stat that doesn't exist, except for maybe equipment?
			if (!map.ContainsKey(stat))
			{
				throw new KeyNotFoundException(String.Format("Stat {0} has not been set.", stat));
			}

			return map[stat];
		}

		public int Set(string stat, int value)
		{
			int old = Get(stat);
			map[stat] = value;

			return old;
		}

		public int Set(Enum stat, int value)
		{
			return Set(stat.ToString(), value);
		}

		new public int this[string stat]
		{
			get
			{
				return base[stat];
			}
			set
			{
				Set(stat, value);
			}
		}

		new public int this[Enum stat]
		{
			get
			{
				return base[stat];
			}
			set
			{
				Set(stat, value);
			}
		}
	}
}
