namespace RPGLibrary.Stats
{
	/// <summary>
	/// Returns a constant value for every stat.
	/// </summary>
	public class StatsConstant : StatsPrototype
	{
		private int value;

		public override int Get(string stat)
		{
			return value;
		}

		public StatsConstant(int value)
		{
			this.value = value;
		}

		public static readonly StatsConstant Zero = new StatsConstant(0);
		public static readonly StatsConstant One = new StatsConstant(1);
	}
}
