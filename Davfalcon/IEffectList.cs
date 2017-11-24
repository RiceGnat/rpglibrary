using System.Collections.Generic;

namespace Davfalcon
{
	public interface IEffectList : IList<IEffectArgs>
	{
		void Add(string name);
		void Add(string name, int value);
		void Add(string name, int value, params object[] args);
	}
}
