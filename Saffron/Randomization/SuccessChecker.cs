using System;

namespace Saffron.Randomization
{
	/// <summary>
	/// Check for success within a given threshold.
	/// </summary>
	public class SuccessChecker : RandomBase, ISuccessCheck
	{
		private double threshold;

		protected virtual double GenerateValue()
		{
			return Generator.NextDouble();
		}

		public bool Check()
		{
			return GenerateValue() <= threshold;
		}

		public SuccessChecker(double threshold)
		{
			if (threshold < 0 || threshold > 1.0)
				throw new ArgumentOutOfRangeException("Success threshold must be greater than or equal to 0.0 and less than or equal to 1.0");
			this.threshold = threshold;
		}
	}
}
