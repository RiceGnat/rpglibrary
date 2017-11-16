namespace Davfalcon.Engine.Combat
{
	public class HitCheck
	{
		public readonly double HitChance;
		public readonly bool Hit;
		public readonly double CritChance;
		public readonly bool Crit;

		public HitCheck(double hitChance, bool hit, double critChance = 0, bool crit = false)
		{
			HitChance = hitChance;
			Hit = hit;
			CritChance = critChance;
			Crit = crit;
		}
	}
}
