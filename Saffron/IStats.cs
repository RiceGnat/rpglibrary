using System;

namespace Saffron
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
		int Get(string stat);

		/// <summary>
		/// Gets a stat by enum name.
		/// </summary>
		/// <param name="stat">The enum for the name of the stat.</param>
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
