using System;

namespace Davfalcon.Revelator
{
	[Serializable]
    public sealed class Unit : UnitTemplate<IUnit>, IUnit
    {
        protected override IUnit Self => this;

		public static IUnit Create(Func<Unit, IUnit> func) => func(new Unit());
	}
}
