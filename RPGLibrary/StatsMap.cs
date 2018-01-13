using System;
using System.Collections.Generic;

namespace RPGLibrary
{
	/// <summary>
	/// Implements data structure for stat lookup.
	/// </summary>
	[Serializable]
	public class StatsMap : StatsPrototype, IEditableStats
	{
		private Dictionary<string, int> map = new Dictionary<string, int>();

		public override int Get(string stat) => map.ContainsKey(stat) ? map[stat] : 0;

		public IEditableStats Set(string stat, int value)
		{
			int old = Get(stat);
			map[stat] = value;
			return this;
		}

		public IEditableStats Set(Enum stat, int value) => Set(stat.ToString(), value);

		new public int this[string stat]
		{
			get => base[stat];
			set => Set(stat, value);
		}

		new public int this[Enum stat]
		{
			get => base[stat];
			set => Set(stat, value);
		}
	}
}
