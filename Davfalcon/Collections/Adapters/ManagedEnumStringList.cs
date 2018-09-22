using System;
using System.Collections.Generic;
using Davfalcon.Collections.Generic;

namespace Davfalcon.Collections.Adapters
{
	[Serializable]
	public class ManagedEnumStringList : ManagedList<EnumString>
	{
		[NonSerialized]
		private IList<Enum> readOnly;

		new public IList<Enum> ReadOnly
		{
			get
			{
				if (readOnly == null)
					readOnly = new EnumStringListAdapter(AsReadOnly());
				return readOnly;
			}
		}
	}
}
