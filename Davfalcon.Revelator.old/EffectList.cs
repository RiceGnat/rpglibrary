using System;
using Davfalcon.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	internal class EffectList : ManagedList<IEffectArgs>, IEffectList
	{
		public void Add(string name)
			=> Add(name, null);

		public void Add(string name, object[] args)
			=> Add(new EffectArgs(name, args));
	}
}
