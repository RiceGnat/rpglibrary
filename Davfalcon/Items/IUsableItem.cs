using System.Collections.Generic;
using RPGLibrary;
using RPGLibrary.Items;

namespace Davfalcon.Items
{
	public interface IUsableItem : IItem
	{
		int Remaining { get; }
		UsableDuringState UsableDuring { get; }
		IList<ILogEntry> Use(IUnit user, params object[] targets);
	}
}
