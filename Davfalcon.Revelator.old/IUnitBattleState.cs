namespace Davfalcon.Revelator
{
	public interface IUnitBattleState
	{
		IBattle Battle { get; }
		int Team { get; }
	}
}
