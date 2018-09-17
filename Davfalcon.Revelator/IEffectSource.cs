using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IEffectSource
	{
		string SourceName { get; }
		IEnumerable<IEffectArgs> Effects { get; }
	}
}
