using System;
using System.Collections.Generic;
using RPGLibrary.Serialization;

namespace RPGLibrary.Collections
{
	[Serializable]
	public class Catalog<T> : List<T> where T : class
	{
		public void AddCopy(T obj)
		{
			Add((T)Serializer.DeepClone(obj));
		}

		public T GetCopy(int index)
		{
			return (T)Serializer.DeepClone(this[index]);
		}
	}
}
