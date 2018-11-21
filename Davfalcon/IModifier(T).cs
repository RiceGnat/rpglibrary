namespace Davfalcon
{
	public interface IModifier<T> : IDescribable
	{
		T Target { get; }

		void Bind(T target);
		void Bind(IModifier<T> target);
	}
}
