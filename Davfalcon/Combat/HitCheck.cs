namespace Davfalcon.Combat
{
	public class HitCheck
	{
		public readonly double SuccessChance;
		public readonly bool Success;

		public HitCheck(double chance, bool success)
		{
			SuccessChance = chance;
			Success = success;
		}
	}
}
