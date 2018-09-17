using System;

namespace Davfalcon.Revelator.Engine.Combat
{
	[Serializable]
	public class Damage : ILogEntry
	{
		public static bool LogFullDamage = true;

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
			return String.Format("{0} deals {1} outgoing {2} {3} damage.", Source, Value, Type, Element);
		}

		public string LogWith(HPLoss hpLoss)
		{
			if (LogFullDamage) return this + Environment.NewLine + hpLoss;
			else return String.Format("{0} deals {1} {2} {3} damage to {4}.", Source, hpLoss.Value, Type, Element, hpLoss.Unit);
		}
	}
}
