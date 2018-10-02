namespace Davfalcon.Revelator.Combat
{
	public class CombatOperations : StatsOperations, ICombatOperations
	{
		public virtual int CalculateHitChance(int hit, int dodge)
			=> hit - dodge;

		protected CombatOperations() { }

		new public static CombatOperations Default { get; } = new CombatOperations();
	}
}
