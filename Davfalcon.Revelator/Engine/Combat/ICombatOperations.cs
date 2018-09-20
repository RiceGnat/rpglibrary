namespace Davfalcon.Revelator.Engine.Combat
{
	public interface ICombatOperations : IStatsResolver
	{
		int CalculateHitChance(int hit, int avoid);
	}
}
