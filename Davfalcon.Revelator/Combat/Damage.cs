using System;
using System.Collections.Generic;
using Davfalcon.Nodes;

namespace Davfalcon.Revelator.Combat
{
	[Serializable]
	public class Damage : ILogEntry
	{
		public IEnumerable<Enum> Types { get; }
		public int Value { get; }
		public string Source { get; }

		public Damage(int value, INameable source, params Enum[] types)
			: this(value, source.Name, types)
		{ }

		public Damage(int value, INameable source, IEnumerable<Enum> types)
			: this(value, source.Name, types)
		{ }

		public Damage(int value, string source, params Enum[] types)
			: this(value, source, types as IEnumerable<Enum>)
		{ }

		public Damage(int value, string source, IEnumerable<Enum> types)
		{
			Types = types.ToNewReadOnlyCollectionSafe();
			Value = value;
			Source = source;
		}

		public static Damage None = new Damage(0, "");

		public override string ToString()
			=> $"{Source} deals {Value} outgoing {String.Join(" ", Types)} damage.";
	}
}
