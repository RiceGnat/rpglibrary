namespace Davfalcon.Revelator.Engine.Combat
{
	public interface ICombatMath : IStatsMath
	{
		int CalculateHitChance(int hit, int avoid);
	}
}
