namespace Davfalcon.Revelator.Combat
{
	public class CombatOperations : StatsResolver, ICombatOperations
	{
		public virtual int CalculateHitChance(int hit, int dodge)
			=> hit - dodge;

		new public static CombatOperations Default { get; } = new CombatOperations();
	}
}
