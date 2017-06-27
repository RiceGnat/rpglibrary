using System.Collections.Generic;

namespace RPGLibrary
{
	public interface IUnitModifierStack : IUnitModifier, IEnumerable<IUnitModifier>
	{
		void Add(IUnitModifier item);
		bool Remove(IUnitModifier item);
	}
}
