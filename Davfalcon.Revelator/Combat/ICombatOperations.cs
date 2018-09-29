namespace Davfalcon.Revelator.Combat
{
	public interface ICombatOperations : IStatsResolver
	{
		int CalculateHitChance(int hit, int avoid);
	}
}
