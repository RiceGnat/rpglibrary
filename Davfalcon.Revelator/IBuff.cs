namespace Davfalcon.Revelator
{
	public interface IBuff<T> : IStatsModifier<T>, IEffectSource where T : IUnit
	{
		int Duration { get; }
		int Remaining { get; }
		bool IsDebuff { get; }
		IUnit Owner { get; set; }

		void Reset();
		bool Tick();
	}

	public interface IBuff : IBuff<IUnit> { }
}
