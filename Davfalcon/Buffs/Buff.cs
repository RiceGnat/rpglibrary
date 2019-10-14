using System;

namespace Davfalcon.Buffs
{
	[Serializable]
	public abstract class Buff<TUnit> : UnitStatsModifier<TUnit>, IBuff<TUnit> where TUnit : IUnitTemplate<TUnit>

	{
		public string Name { get; }
		public string Description { get; }

		public int Duration { get; }
		public int Remaining { get; }
	}
}
