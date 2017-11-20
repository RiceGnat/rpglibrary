using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	public interface IBuff : ITimedModifier, IEffectSource, IAutoCatalogable
	{
		string Source { get; set; }
		bool IsDebuff { get; }
	}
}
