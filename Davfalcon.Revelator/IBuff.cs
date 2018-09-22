using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IBuff : ITimedModifier, IEffectSource
	{
		bool IsDebuff { get; }
	}
}
