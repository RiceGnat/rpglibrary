namespace Davfalcon
{
	public interface IUnitBattleState
	{
		IBattle Battle { get; }
		int Team { get; }
	}
}
