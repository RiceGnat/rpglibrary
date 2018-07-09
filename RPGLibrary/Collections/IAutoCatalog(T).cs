namespace RPGLibrary.Collections.Generic
{
	public interface IAutoCatalog<T> : ICatalog<T> where T : INameable
	{
		void Load(T entry);
	}
}
