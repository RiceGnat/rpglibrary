namespace Davfalcon.Revelator
{
	public interface IEffect : INameable
	{
		void Resolve(IUnit unit, IUnit target, params object[] args);
	}
}
