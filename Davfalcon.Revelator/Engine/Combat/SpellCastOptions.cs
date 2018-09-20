namespace Davfalcon.Revelator.Engine.Combat
{
	public struct SpellCastOptions
	{
		public int AdjustedCost { get; private set; }
		public int DamageMultiplier { get; private set; }
		public bool NoScaling { get; private set; }
	}
}
