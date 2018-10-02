namespace Davfalcon.Revelator.Combat
{
	public interface ICombatOperations : IStatsOperations
	{
		int CalculateHitChance(int hit, int avoid);
	}
}
