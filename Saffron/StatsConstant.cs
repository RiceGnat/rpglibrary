﻿namespace Saffron
{
	/// <summary>
	/// Returns a constant value for every stat.
	/// </summary>
	public class StatsConstant : StatsPrototype
	{
		private int value;

		/// <summary>
		/// Gets a stat by string name.
		/// </summary>
		/// <param name="stat">The name of the stat.</param>
		/// <returns>The constant value of the stat.</returns>
		public override int Get(string stat) => value;

		/// <summary>
		/// Initializes a new <see cref="StatsConstant"/> that returns a constant value for every stat.
		/// </summary>
		/// <param name="value">The constant value to return.</param>
		public StatsConstant(int value) => this.value = value;

		/// <summary>
		/// A singleton instance of <see cref="StatsConstant"/> that always returns 0.
		/// </summary>
		public static readonly StatsConstant Zero = new StatsConstant(0);

		/// <summary>
		/// A singleton instance of <see cref="StatsConstant"/> that always returns 1.
		/// </summary>
		public static readonly StatsConstant One = new StatsConstant(1);
	}
}
