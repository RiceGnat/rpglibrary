namespace Davfalcon
{
	public interface IStatsContainer
	{
		/// <summary>
		/// Gets the unit's stats.
		/// </summary>
		IStats Stats { get; }

		/// <summary>
		/// Gets a breakdown of the unit's stats.
		/// </summary>
		IStatsDetails StatsDetails { get; }
	}
}
