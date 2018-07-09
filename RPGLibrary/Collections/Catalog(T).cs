using System.Collections.Generic;
using RPGLibrary.Serialization;

namespace RPGLibrary.Collections.Generic
{
	public class Catalog<T> : IAutoCatalog<T> where T : INameable
	{
		private Dictionary<string, T> lookup = new Dictionary<string, T>();

		public IEnumerable<string> Keys => lookup.Keys;
		public IEnumerable<T> Entries => lookup.Values;

		public T Get(string name)
			=> (T)Serializer.DeepClone(lookup[name]);

		public void Load(T entry)
			=> Load(entry.Name, entry);

		public void Load(string name, T entry)
			=> lookup.Add(name, entry);
	}
}
