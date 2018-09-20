using System;

namespace Davfalcon.Revelator.Engine.Combat
{
	[Serializable]
	public struct PointLoss : ILogEntry
	{
		public readonly string Unit;
		public readonly Enum Stat;
		public readonly int Value;

		public PointLoss(string unit, Enum stat, int value)
		{
			Unit = unit;
			Stat = stat;
			Value = value;
		}

		public override string ToString()
		{
			return String.Format("{0} loses {1} HP.", Unit, Value);
		}
	}
}
