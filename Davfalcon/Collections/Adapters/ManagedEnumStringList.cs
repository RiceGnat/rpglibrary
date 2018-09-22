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

		new public IList<Enum> AsReadOnly()
		{
			if (readOnly == null)
				readOnly = new EnumStringListAdapter(base.AsReadOnly());
			return readOnly;
		}
	}
}
