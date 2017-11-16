using System.Collections.Generic;
using RPGLibrary;
using RPGLibrary.Items;

namespace Davfalcon.Engine.Items
{
	public interface IUsableItem : IItem
	{
		bool IsConsumable { get; }
		int Remaining { get; set; }
		UsableDuringState UsableDuring { get; }
		IList<ILogEntry> Use(IUnit user, params object[] targets);
	}
}
