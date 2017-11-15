using System;

namespace Davfalcon.Items
{
	[Flags]
	public enum UsableDuringState : short
	{
		OutOfCombat = 0,
		InCombat = 1
	}
}
