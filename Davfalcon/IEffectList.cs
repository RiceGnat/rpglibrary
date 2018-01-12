using System.Collections.Generic;

namespace Davfalcon
{
	public interface IEffectList : IList<IEffectArgs>
	{
		void Add(string name);
		void Add(string name, params object[] args);
	}
}
