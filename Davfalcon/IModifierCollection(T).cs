using System.Collections.Generic;

namespace Davfalcon
{
	public interface IModifierCollection<T> : IModifier<T>, IEnumerable<IModifier<T>>
	{
		int Count { get; }
		void Add(IModifier<T> item);
		bool Remove(IModifier<T> item);
		void RemoveAt(int index);
		void Clear();
	}
}
