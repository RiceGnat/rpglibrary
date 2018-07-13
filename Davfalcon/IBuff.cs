using System.Collections.Generic;
using Saffron;

namespace Davfalcon
{
	public interface IBuff : ITimedModifier, IEffectSource
	{
		string Source { get; set; }
		bool IsDebuff { get; }
		int SuccessChance { get; set; }
	}
}
