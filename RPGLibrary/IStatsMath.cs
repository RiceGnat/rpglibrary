namespace RPGLibrary
{
	/// <summary>
	/// Specifies a format for defining stat math.
	/// </summary>
	public interface IStatsMath
	{
		/// <summary>
		/// Perform a calculation on the given parameters.
		/// </summary>
		/// <param name="a">Value to be added.</param>
		/// <param name="b">Value to be added.</param>
		/// <param name="m">Multiplication factor.</param>
		/// <returns></returns>
		int Calculate(int a, int b, int m);
	}
}
