using System.Collections.Generic;
using Saffron.Serialization;

namespace Saffron.Collections.Generic
{
	public class PrototypeCloner<T> : IRegistry<T>
	{
		private Dictionary<string, T> lookup = new Dictionary<string, T>();

		public void Register(T prototype, string name)
			=> lookup.Add(name, prototype);

		public T Get(string name)
			=> (T)Serializer.DeepClone(lookup[name]);
	}
}
