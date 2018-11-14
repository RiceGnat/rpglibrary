using System;

namespace Davfalcon
{
	public class UnitStatsModifier : UnitStatsModifier<IUnit>
	{
		protected override IUnit InterfaceUnit { get; }

		public UnitStatsModifier()
		{
			InterfaceUnit = new Decorator(this);
		}
	}
}
