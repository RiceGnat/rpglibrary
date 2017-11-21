﻿using System.Collections.Generic;
using RPGLibrary.Serialization;

namespace Davfalcon.Engine
{
	internal class Catalog<T> : IAutoCatalog<T> where T : IAutoCatalogable
	{
		private Dictionary<string, T> lookup = new Dictionary<string, T>();

		public T Get(string name)
			=> (T)Serializer.DeepClone(lookup[name]);

		public void Load(T entry)
			=> Load(entry.CatalogKey, entry);

		public void Load(string name, T entry)
			=> lookup.Add(name, entry);
	}
}