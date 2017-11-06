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

		/// <summary>
		/// Sets a stat by string name.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		IStatsEditable Set(string stat, int value);

		/// <summary>
		/// Sets a stat by enum name.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		IStatsEditable Set(Enum stat, int value);
	}
}
