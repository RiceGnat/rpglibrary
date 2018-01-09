using System.Collections.Generic;

namespace RPGLibrary.Collections.Generic
{
	public interface ICatalog<T>
	{
		IEnumerable<string> Keys { get; }
		IEnumerable<T> Entries { get; }

		void Load(string name, T entry);
		T Get(string name);
	}
}
