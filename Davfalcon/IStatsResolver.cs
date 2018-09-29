namespace Davfalcon
{
	/// <summary>
	/// Specifies operations used to calculate stats.
	/// </summary>
	public interface IStatsResolver
	{
		/// <summary>
		/// Gets the seed value for the aggregation function.
		/// </summary>
		int AggregateSeed { get; }

		/// <summary>
		/// Aggregation function for multiplicative values.
		/// </summary>
		/// <param name="a">The accumulated value.</param>
		/// <param name="b">The value be added to the accumulator.</param>
		/// <returns>The new accumulated value.</returns>
		int Aggregate(int a, int b);

		/// <summary>
		/// Scales a value by another value. Used to define how multiplicative modifiers are applied.
		/// </summary>
		/// <param name="a">The base value.</param>
		/// <param name="b">The scale factor.</param>
		/// <returns>The scaled value.</returns>
		int Scale(int a, int b);

		/// <summary>
		/// Inversely scales a value by another value.
		/// </summary>
		/// <param name="a">The base value.</param>
		/// <param name="b">The scale factor.</param>
		/// <returns>The scaled value.</returns>
		int ScaleInverse(int a, int b);

		/// <summary>
		/// Perform a calculation on the given operands.
		/// </summary>
		/// <param name="a">Value to be added.</param>
		/// <param name="b">Value to be added.</param>
		/// <param name="m">Multiplication factor.</param>
		int Calculate(int a, int b, int m);
	}
}
