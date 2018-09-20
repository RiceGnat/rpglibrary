using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Davfalcon.Revelator.Engine.Combat
{
	[Serializable]
	public class Damage : ILogEntry
	{
		public static bool LogFullDamage = true;

		public readonly IEnumerable<Enum> Types;
		public readonly int Value;
		public readonly string Source;

		public Damage(int value, string source, params Enum[] types)
		{
			Types = new ReadOnlyCollection<Enum>(types);
			Value = value;
			Source = source;
		}

		public Damage(int value, string source, IEnumerable<Enum> types)
			: this(value, source, types.ToArray())
		{ }

		public override string ToString()
			=> String.Format($"{Source} deals {Value} outgoing {String.Join(" ", Types)} damage.");

		public string LogWith(HPLoss hpLoss)
		{
			if (LogFullDamage) return this + Environment.NewLine + hpLoss;
			else return String.Format($"{hpLoss.Unit} takes {hpLoss.Value} {String.Join(" ", Types)} damage from {Source}.");
		}
	}
}
