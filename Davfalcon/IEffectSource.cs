using System.Collections.Generic;

namespace Davfalcon
{
	public interface IEffectSource
	{
		string SourceName { get; }
		IEnumerable<IEffectArgs> Effects { get; }
	}
}
