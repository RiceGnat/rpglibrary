using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGLibrary.Collections.Generic;

namespace Davfalcon.Engine
{
	public interface IAutoCatalog<T> : ICatalog<T> where T : IAutoCatalogable
	{
		void Load(T entry);
	}
}
