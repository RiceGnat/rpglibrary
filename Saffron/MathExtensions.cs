using System;

namespace Saffron
{
	public static class MathExtensions
	{
		public static int Clamp(this int value, int min, int max)
			=> Math.Max(min, Math.Min(value, max));

		public static int Scale(this int value, int factor)
		{
			float f;

			if (factor == 0)
			{
				f = 1;
			}
			else if (factor >= 0)
			{
				f = 1 + factor / 100f;
			}
			else
			{
				f = 100f / (100 - factor);
			}

			return (int)(value * f);
		}

		public static int Cap(this int value, int cap, int percent = 0)
		{
			int v = value;
			if (percent > 0) v = v * percent / 100;
			if (cap > 0) v = Math.Min(v, cap);
			return v;
		}
	}
}
