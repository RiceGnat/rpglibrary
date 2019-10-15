namespace Davfalcon.Revelator
{
	public interface IUnitTemplate<TUnit> : Davfalcon.IUnitTemplate<TUnit>
		where TUnit : class, IUnitTemplate<TUnit>
    {
    }
}
