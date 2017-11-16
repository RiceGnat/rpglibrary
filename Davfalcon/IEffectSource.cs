using System.Collections.Generic;

namespace Davfalcon
{
	public interface IEffectSource
	{
		string SourceName { get; }
		ICollection<KeyValuePair<string, int>> Effects { get; }
	}
}
