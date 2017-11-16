namespace RPGLibrary.Collections.Generic
{
	public interface ICatalog<T>
	{
		void Load(string name, T entry);
		T Get(string name);
	}
}
