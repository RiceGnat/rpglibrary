using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IEffectSource : INameable
	{
		string Owner { get; set; }
		IEnumerable<IEffect> Effects { get; }
	}
}
