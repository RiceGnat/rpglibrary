﻿using System;

namespace Davfalcon
{
	public enum DamageType
	{
		Physical, Magical, True
	}
}

namespace Davfalcon.Combat
{
	[Serializable]
	public class Damage : ILogEntry
	{
		public readonly DamageType Type;
		public readonly Element Element;
		public readonly int Value;
		public readonly string Source;

		public Damage(DamageType type, Element element, int value, string source)
		{
			Type = type;
			Element = element;
			Value = value;
			Source = source;
		}

		public override string ToString()
		{
			return String.Format("{0} deals {1} {2} {3} damage.", Source, Value, Type, Element);
		}
	}
}
