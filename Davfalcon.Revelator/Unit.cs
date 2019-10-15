using System;

namespace Davfalcon.Revelator
{
	[Serializable]
	public sealed class Unit : UnitTemplate<IUnit>, IUnit
	{
		protected override IUnit Self => this;

		public static IUnit Create(Func<Unit, IUnit> func) => func(new Unit());

		public static IUnit Modify(IUnit unit, Func<Unit, IUnit> func)
		{
			if (unit is Unit _unit) return func(_unit);
			else throw new ArgumentException($"Unit must be an instance of {typeof(Unit).FullName}", nameof(unit));
		}
	}
}
