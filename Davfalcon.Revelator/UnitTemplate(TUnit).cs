using System;

namespace Davfalcon.Revelator
{
	[Serializable]
    public abstract class UnitTemplate<TUnit> : Davfalcon.UnitTemplate<TUnit>, IUnitTemplate<TUnit>
		where TUnit : class, IUnitTemplate<TUnit>
    {
		// Add new properties here
    }
}
