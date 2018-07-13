using System;

namespace Saffron
{
	/// <summary>
	/// Exposes methods to access and edit stats.
	/// </summary>
	public interface IEditableStats : IStats
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
		/// <returns>This <see cref="IEditableStats"/> instance. Used for chaining methods.</returns>
		IEditableStats Set(string stat, int value);

		/// <summary>
		/// Sets a stat by enum name.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		/// <param name="value">The value of the stat.</param>
		/// <returns>This <see cref="IEditableStats"/> instance. Used for chaining methods.</returns>
		IEditableStats Set(Enum stat, int value);
	}
}
