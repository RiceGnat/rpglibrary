using System;

namespace RPGLibrary
{
	public static class MathExtensions
	{
		public static int Clamp(this int number, int min, int max)
		{
			return Math.Max(min, Math.Min(number, max));
		}
	}
}
