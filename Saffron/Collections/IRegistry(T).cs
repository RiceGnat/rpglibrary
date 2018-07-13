namespace Saffron.Collections.Generic
{
	public interface IRegistry<T>
	{
		void Register(T prototype, string name);
		T Get(string name);
	}
}
