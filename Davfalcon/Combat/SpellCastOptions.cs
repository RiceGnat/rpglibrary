namespace Davfalcon.Combat
{
	public class SpellCastOptions
	{
		public int AdjustedCost { get; set; }
		public int DamageMultiplier { get; set; }

		public SpellCastOptions()
		{
			AdjustedCost = -1;
			DamageMultiplier = -1;
		}
	}
}
