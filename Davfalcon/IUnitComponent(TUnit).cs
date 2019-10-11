namespace Davfalcon
{
	public interface IUnitComponent<TUnit> where TUnit : IUnitTemplate<TUnit>
	{
		void Initialize(TUnit unit);
	}
}
