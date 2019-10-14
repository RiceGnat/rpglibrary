using System;

namespace Davfalcon.Buffs
{
	[Serializable]
	public abstract class Buff<TUnit> : UnitStatsModifier<TUnit>, IBuff<TUnit> where TUnit : class, IUnitTemplate<TUnit>
	{
		public string Name { get; }

		public string Description { get; }

		public bool IsDebuff { get; }

		public int Duration { get; }

		public int Remaining { get; set; }
	}
}
