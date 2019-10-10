using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IBuff : ITimedModifier, IEffectSource
	{
		string Source { get; set; }
		bool IsDebuff { get; }
		int SuccessChance { get; set; }
	}
}
