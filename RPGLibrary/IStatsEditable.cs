using System;

namespace RPGLibrary
{
	public interface IStatsEditable : IStats
	{
		/// <summary>
		/// Gets or sets a stat by string name.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		new int this[string stat] { get; set; }

		/// <summary>
		/// Gets or sets a stat by enum name.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		new int this[Enum stat] { get; set; }
	}
}
