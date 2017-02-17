using System;

namespace RPGLibrary
{
	/// <summary>
	/// Exposes methods to access stats.
	/// </summary>
	public interface IStats
	{
		/// <summary>
		/// Gets a stat by string name.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		/// <returns>The value of the stat.</returns>
		int Get(string stat);

		/// <summary>
		/// Gets a stat by enum name.
		/// </summary>
		/// <param name="stat">The enum for the name of the stat.</param>
		/// <returns>The value of the stat.</returns>
		int Get(Enum stat);

		/// <summary>
		/// Gets a stat by string name.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		/// <returns>The value of the stat.</returns>
		int this[string stat] { get; }

		/// <summary>
		/// Gets a stat by enum name.
		/// </summary>
		/// <param name="stat">The enum for the name of the stat.</param>
		/// <returns>The value of the stat.</returns>
		int this[Enum stat] { get; }
	}
}
