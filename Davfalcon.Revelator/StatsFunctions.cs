using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public static class StatsFunctions
	{
		public static int Resolve(int baseValue, IReadOnlyDictionary<Enum, int> modifications)
				=> modifications[StatModType.Additive] + baseValue.Scale(modifications[StatModType.Scaling]);

		public static Func<int, int, int> GetAggregator(Enum modificationType) => (a, b) => a + b;

		public static int GetAggregatorSeed(Enum modificationType) => 0;
	}
}
