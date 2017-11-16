using System;

namespace Davfalcon.Engine
{
	// This class needs a better name
	public class Data
	{
		public static Data Current { get; private set; } = new Data();
		public static void SetData(Data newData)
		{
			if (newData == null) throw new ArgumentNullException("Data object cannot be set to null.");
			Current = newData;
		}

		public IEffectFactory Effects { get; private set; } = new EffectFactory();

	}
}
