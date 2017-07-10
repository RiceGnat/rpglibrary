using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RPGLibrary;

namespace Davfalcon
{
	public enum DamageType
	{
		Physical, Magical, True
	}

	public enum Element
	{
		Neutral, Fire, Lightning, Ice, Earth, Wind, Divine, Dark
	}

	public struct Damage : ILogEntry
	{
		public DamageType Type;
		public Element Element;
		public int Value;
		public string Source;

		public override string ToString()
		{
			return String.Format("{0} deals {1} {2} {3} damage.", Source, Value, Type, Element);
		}
	}
}
