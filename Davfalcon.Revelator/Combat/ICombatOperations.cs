namespace Davfalcon.Revelator.Combat
{
	public interface ICombatOperations : IMathOperations
	{
		int CalculateHitChance(int hit, int avoid);
	}
}
