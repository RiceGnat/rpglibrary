using System;
using System.Collections.Generic;

namespace Saffron.Collections.Generic
{
	[Serializable]
	public class ManagedList<T> : List<T>
	{
		public IList<T> ReadOnly { get; protected set; }

		public ManagedList()
		{
			ReadOnly = AsReadOnly();
		}
	}
}
