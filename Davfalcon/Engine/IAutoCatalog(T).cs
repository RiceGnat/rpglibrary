using RPGLibrary;
using RPGLibrary.Collections.Generic;

namespace Davfalcon.Engine
{
	public interface IAutoCatalog<T> : ICatalog<T> where T : INameable
	{
		void Load(T entry);
	}
}
