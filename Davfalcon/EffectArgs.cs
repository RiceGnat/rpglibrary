using System;

namespace Davfalcon
{
	[Serializable]
	public class EffectArgs : IEffectArgs
	{
		public string Name { get; private set; }
		//public int Value { get; private set; }
		public object[] Args { get; private set; }

		public EffectArgs(string name, object[] args)
		{
			Name = name;
			//Value = value;
			Args = args ?? new object[] { };
		}
	}
}
