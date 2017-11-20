using System;
using RPGLibrary.Collections.Generic;

namespace Davfalcon.Engine
{
	// This class needs a better name
	public class System
	{
		public static System Current { get; private set; } = new System();
		public static void SetSystem(System system)
		{
			if (system == null) throw new ArgumentNullException("System object cannot be set to null.");
			Current = system;
		}

		public IEffectFactory Effects { get; private set; } = new EffectFactory();
		public ICatalog<IBuff> Buffs { get; private set; }
	}
}
