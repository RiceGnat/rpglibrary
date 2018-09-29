namespace Davfalcon.Revelator.Engine.Combat
{
	public class SpellCastOptions
	{
		public int CostOverride { get; private set; } = -1;
		public bool ScaleDamage { get; private set;  } = true;
		public bool UseAttack { get; private set; } = false;

		public static SpellCastOptions Default { get; }

		public class Builder : BuilderBase<SpellCastOptions>
		{
			public Builder SetCost(int cost)
			{
				build.CostOverride = cost;
				return this;
			}
		}
	}
}
