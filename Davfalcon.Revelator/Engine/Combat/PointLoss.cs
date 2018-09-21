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

		public PointLoss(IUnit unit, Enum stat, int value)
			: this(unit.Name, stat, value)
		{ }


		public override string ToString()
		{
			return String.Format($"{Unit} loses {1} HP.", Unit, Value);
		}
	}
}
