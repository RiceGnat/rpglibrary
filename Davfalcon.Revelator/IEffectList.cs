using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IEffectList : IList<IEffectArgs>
	{
		void Add(string name);
		void Add(string name, params object[] args);
	}
}
