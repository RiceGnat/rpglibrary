using System;
using System.Collections.Generic;
using RPGLibrary.Collections.Generic;

namespace Davfalcon.Engine.Management.Setup
{
	[Serializable]
	internal class EffectList : ManagedList<KeyValuePair<string, int>>, IEffectList
	{
		public void Add(string name, int value)
			=> Add(new KeyValuePair<string, int>(name, value));
	}
}
