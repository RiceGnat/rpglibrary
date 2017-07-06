using System;
using System.Collections.Generic;
using RPGLibrary.Serialization;

namespace RPGLibrary.Collections
{
	[Serializable]
	public class Catalog<T> : List<T>
	{
		public void AddCopy(T obj)
		{
			Add(Serializer.DeepClone<T>(obj));
		}

		public T GetCopy(int index)
		{
			return Serializer.DeepClone<T>(this[index]);
		}
	}
}
