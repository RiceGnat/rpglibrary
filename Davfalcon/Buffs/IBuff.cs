namespace Davfalcon.Buffs
{
	public interface IBuff<TUnit> : IStatsModifier<TUnit>
		where TUnit : IUnitTemplate<TUnit>
	{
		string Name { get; }

		string Description { get; }

		int Duration { get; }

		int Remaining { get; }
	}
}
