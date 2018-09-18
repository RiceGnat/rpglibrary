﻿using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Buff : TimedModifier, IBuff
	{
		public string Source { get; set; }
		public bool IsDebuff { get; set; }
		public int SuccessChance { get; set; }

		private EffectList effects = new EffectList();
		public IEffectList UpkeepEffects { get { return effects; } }
		IEnumerable<IEffectArgs> IEffectSource.Effects { get { return effects.ReadOnly; } }
		string IEffectSource.SourceName { get { return Name; } }

		public Buff() : base() { }

		public Buff(string name) : base()
		{
			Name = name;
		}
	}
}