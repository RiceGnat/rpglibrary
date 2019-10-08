using System;

namespace Davfalcon.Stats
{
	/// <summary>
	/// Exposes methods to access and edit stats.
	/// </summary>
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
		/// <param name="value">The value of the stat.</param>
		/// <returns>This <see cref="IStatsEditable"/> instance. Used for chaining methods.</returns>
		IStatsEditable Set(string stat, int value);

		/// <summary>
		/// Sets a stat by enum name.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		/// <param name="value">The value of the stat.</param>
		/// <returns>This <see cref="IStatsEditable"/> instance. Used for chaining methods.</returns>
		IStatsEditable Set(Enum stat, int value);
	}
}
