using System;
using System.Collections.Generic;
using Davfalcon.Buffs;

namespace Davfalcon.Revelator
{
	[Serializable]
	public sealed class Buff : Buff<IUnit>, IBuff, IUnit
	{
		protected override IUnit SelfAsUnit => this;

		public string Source { get; set; }
		public IUnit Owner { get; set; }

		protected override int Resolve(int baseValue, IReadOnlyDictionary<Enum, int> modifications) => StatsFunctions.Resolve(baseValue, modifications);

		protected override Func<int, int, int> GetAggregator(Enum modificationType) => StatsFunctions.GetAggregator(modificationType);

		protected override int GetAggregatorSeed(Enum modificationType) => StatsFunctions.GetAggregatorSeed(modificationType);
	}
}
