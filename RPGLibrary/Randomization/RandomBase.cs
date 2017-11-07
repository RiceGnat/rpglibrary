using System;

namespace RPGLibrary.Randomization
{
	/// <summary>
	/// Abstract base class for generating random values.
	/// </summary>
	public abstract class RandomBase
	{
		private static readonly Random random = new Random();

		protected Random Generator
		{
			get { return random; }
		}
	}
}
