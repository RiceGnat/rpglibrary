namespace Davfalcon.Revelator.Engine.Combat
{
	public class SpellCastOptions
	{
		public int AdjustedCost { get; set; }
		public int DamageMultiplier { get; set; }
		public bool NoScaling { get; set; }

		public SpellCastOptions()
		{
			AdjustedCost = -1;
			DamageMultiplier = -1;
			NoScaling = false;
		}
	}
}
