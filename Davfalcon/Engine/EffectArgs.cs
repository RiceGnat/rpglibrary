using System;

namespace Davfalcon.Engine
{
	[Serializable]
	public class EffectArgs : IEffectArgs
	{
		public string Name { get; private set; }
		public int Value { get; private set; }
		public object[] Args { get; private set; }

		public EffectArgs(string name, int value, object[] args)
		{
			Name = name;
			Value = value;
			Args = args;
		}

		public EffectArgs(string name, int value) : this(name, value, null) { }
	}
}
