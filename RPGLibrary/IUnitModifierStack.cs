using System.Collections.Generic;

namespace RPGLibrary
{
	public interface IUnitModifierStack : IUnitModifier, IEnumerable<IUnitModifier>
	{
		int Count { get; }
		void Add(IUnitModifier item);
		bool Remove(IUnitModifier item);
		void Clear();
	}
}
