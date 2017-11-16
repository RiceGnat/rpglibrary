using System.Collections.Generic;

namespace Davfalcon
{
	public interface IEffectList : IList<KeyValuePair<string, int>>
	{
		void Add(string name, int value = 0);
	}
}
