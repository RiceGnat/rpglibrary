namespace Davfalcon.Revelator
{
    public abstract class UnitTemplate<TUnit> : Davfalcon.UnitTemplate<TUnit>, IUnitTemplate<TUnit> where TUnit : IUnitTemplate<TUnit>
    {
    }
}
