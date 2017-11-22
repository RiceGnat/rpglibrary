using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	public interface IUnitInventoryProperties : IUnitProperties
	{
		IList<IItem> Inventory { get; }
	}
}
