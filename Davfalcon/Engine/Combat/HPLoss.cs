using System;

namespace Davfalcon.Engine.Combat
{
	[Serializable]
	public class HPLoss : ILogEntry
	{
		public readonly string Unit;
		public readonly int Value;

		public HPLoss(string unit, int value)
		{
			Unit = unit;
			Value = value;
		}

		public override string ToString()
		{
			return String.Format("{0} loses {1} HP.", Unit, Value);
		}
	}
}
