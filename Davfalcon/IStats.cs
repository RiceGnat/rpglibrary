using System;

namespace Davfalcon
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
		/// <returns>The value of the stat if it exists; otherwise, 0.</returns>
		int Get(string stat);

        /// <summary>
        /// Gets a stat by enum name.
        /// </summary>
        /// <param name="stat">The enum for the name of the stat.</param>
        /// <returns>The value of the stat if it exists; otherwise, 0.</returns>
        int Get(Enum stat);

        /// <summary>
        /// Gets a stat by string name.
        /// </summary>
        /// <param name="stat">The name of the stat.</param>
        int this[string stat] { get; }

        /// <summary>
        /// Gets a stat by enum name.
        /// </summary>
        /// <param name="stat">The enum for the name of the stat.</param>
        int this[Enum stat] { get; }
	}
}
