namespace Davfalcon.Revelator.Engine.Combat
{
	public class SpellCastOptions
	{
		public int CostOverride { get; private set; } = -1;
		public bool ScaleDamage { get; private set;  } = true;
		public bool UseAttack { get; private set; } = false;

		public class Builder
		{
			private SpellCastOptions options;

			public Builder()
			{
				options = new 
			}
		}
	}
}
