namespace Davfalcon.Revelator.Engine.Combat
{
	public class CombatMath : StatsMath, ICombatMath
	{
		public virtual int CalculateHitChance(int hit, int dodge)
			=> hit - dodge;

		public static CombatMath Default = new CombatMath();
	}
}
