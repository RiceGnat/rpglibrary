namespace Davfalcon.Buffs
{
	public interface IBuff<TUnit> : IStatsModifier<TUnit> where TUnit : class, IUnitTemplate<TUnit>
	{
		string Name { get; }

		string Description { get; }

		bool IsDebuff { get; }

		int Duration { get; }

		int Remaining { get; set; }
	}
}
