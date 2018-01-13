﻿using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	public interface IBuff : ITimedModifier, IEffectSource
	{
		string Source { get; set; }
		bool IsDebuff { get; }
	}
}
