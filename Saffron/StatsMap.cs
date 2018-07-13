using System;
using System.Collections.Generic;

namespace Saffron
{
	/// <summary>
	/// Implements data structure for stat lookup.
	/// </summary>
	[Serializable]
	public class StatsMap : StatsPrototype, IEditableStats
	{
		private Dictionary<string, int> map = new Dictionary<string, int>();

		/// <summary>
		/// Gets a stat by string name.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		/// <returns>The value of the stat if it exists; otherwise, 0.</returns>
		public override int Get(string stat) => map.ContainsKey(stat) ? map[stat] : 0;

		/// <summary>
		/// Sets a stat by string name.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		/// <param name="value">The value of the stat.</param>
		/// <returns>This <see cref="IEditableStats"/> instance. Used for chaining methods.</returns>
		public IEditableStats Set(string stat, int value)
		{
			int old = Get(stat);
			map[stat] = value;
			return this;
		}

		/// <summary>
		/// Sets a stat by enum name.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		/// <param name="value">The value of the stat.</param>
		/// <returns>This <see cref="IEditableStats"/> instance. Used for chaining methods.</returns>
		public IEditableStats Set(Enum stat, int value) => Set(stat.ToString(), value);

		/// <summary>
		/// Gets or sets a stat by string name.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		new public int this[string stat]
		{
			get => base[stat];
			set => Set(stat, value);
		}

		/// <summary>
		/// Gets a stat by enum name.
		/// </summary>
		/// <param name="stat">The enum for the name of the stat.</param>
		new public int this[Enum stat]
		{
			get => base[stat];
			set => Set(stat, value);
		}
	}
}
