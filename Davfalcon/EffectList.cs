using System;
using RPGLibrary.Collections.Generic;

namespace Davfalcon
{
	[Serializable]
	internal class EffectList : ManagedList<IEffectArgs>, IEffectList
	{
		public void Add(string name)
			=> Add(name, 0);

		public void Add(string name, int value)
			=> Add(name, value, null);

		public void Add(string name, int value, object[] args)
			=> Add(new EffectArgs(name, value, args));
	}
}
