namespace RPGLibrary.Stats
{
	public class StatsMath : StatsPrototype
	{
		private IStats original;
		private IStats additions;
		private IStats multiplications;

		public override int Get(string stat)
		{
			float m;

			if (multiplications[stat] == 0)
			{
				m = 1;
			}
			else if (multiplications[stat] >= 0)
			{
				m = 1 + multiplications[stat] / 100f;
			}
			else
			{
				m = 100f / (100 - multiplications[stat]);
			}

			return (int)((original[stat] + additions[stat]) * m);
		}

		public StatsMath(IStats original, IStats additions, IStats multiplications)
		{
			this.original = original;
			this.additions = additions;
			this.multiplications = multiplications;
		}
	}
}
