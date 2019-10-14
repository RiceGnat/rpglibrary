using System;

namespace Davfalcon.Buffs
{
	public interface IBuffTemplate<TBuff, TUnit, TEvent> : IStatsModifier<TUnit>
		where TBuff : IBuffTemplate<TBuff, TUnit, TEvent>
		where TUnit : IUnitTemplate<TUnit>
	{
		string Name { get; }

		string Description { get; }

		int Duration { get; }

		int Remaining { get; }

		event Action<TBuff, TUnit, TEvent> BuffEvent;
	}
}
