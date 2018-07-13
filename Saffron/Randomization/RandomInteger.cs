using System;

namespace Saffron.Randomization
{
	/// <summary>
	/// Generate random integers.
	/// </summary>
	public class RandomInteger : RandomBase
	{
		private int min;
		private int max;

		public int Get()
		{
			return Generator.Next(min, max);
		}

		public RandomInteger() : this(0, Int32.MaxValue)
		{

		}

		public RandomInteger(int max) : this(0, max)
		{

		}

		public RandomInteger(int min, int max)
		{
			this.min = min;
			this.max = max;
		}
	}
}
