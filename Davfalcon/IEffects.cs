using System.Collections.Generic;

namespace Davfalcon
{
	public interface IEffects
	{
		string SourceName { get; }
		ICollection<KeyValuePair<string, int>> Effects { get; }
	}
}
