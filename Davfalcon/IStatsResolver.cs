namespace Davfalcon
{
	/// <summary>
	/// Specifies a formula to use to calculate stats
	/// </summary>
	public interface IStatsResolver
	{
		int AggregateSeed { get; }

		int Aggregate(int a, int b);

		int Scale(int a, int b);

		int ScaleInverse(int a, int b);

		/// <summary>
		/// Perform a calculation on the given parameters.
		/// </summary>
		/// <param name="a">Value to be added.</param>
		/// <param name="b">Value to be added.</param>
		/// <param name="m">Multiplication factor.</param>
		int Calculate(int a, int b, int m);
	}
}
