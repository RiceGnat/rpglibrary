using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IEffectList : IList<EffectArgs>
	{
		void Add(string name);
		void Add(string name, params object[] args);
	}
}
