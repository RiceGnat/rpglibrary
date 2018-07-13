namespace Saffron.Randomization
{
	/// <summary>
	/// Check for success within a given threshold using a center weighted distribution.
	/// </summary>
	public class CenterWeightedChecker : SuccessChecker
	{
		protected override double GenerateValue()
		{
			return (Generator.NextDouble() + Generator.NextDouble()) / 2;
		}

		public CenterWeightedChecker(double threshold) : base(threshold) { }
	}
}
