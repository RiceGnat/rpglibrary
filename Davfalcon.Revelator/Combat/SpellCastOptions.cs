using Davfalcon.Builders;

namespace Davfalcon.Revelator.Combat
{
	public class SpellCastOptions
	{
		public int CostOverride { get; private set; } = -1;
		public bool ScaleDamage { get; private set;  } = true;
		public bool UseAttack { get; private set; } = false;

		public static SpellCastOptions Default { get; }

		public class Builder : BuilderBase<SpellCastOptions, Builder>
		{
			public override Builder Reset() => Reset(new SpellCastOptions());

			public Builder SetCost(int cost) => Self(o => o.CostOverride = cost);
		}
	}
}
