using System;
using System.Collections.Generic;

namespace RPGLibrary
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
			if (!map.ContainsKey(stat))
			{
				return 0;
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
