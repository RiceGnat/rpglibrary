﻿using System;
using System.Collections.Generic;
using RPGLibrary;
using RPGLibrary.Collections.Generic;

namespace Davfalcon
{
	[Serializable]
	public class Buff : TimedModifier, IBuff
	{
		public string Source { get; set; }
		public bool IsDebuff { get; set; }

		private EffectList effects = new EffectList();
		public IEffectList UpkeepEffects { get { return effects; } }
		ICollection<KeyValuePair<string, int>> IEffectSource.Effects { get { return effects.ReadOnly; } }
		string IEffectSource.SourceName { get { return Name; } }
	}
}
