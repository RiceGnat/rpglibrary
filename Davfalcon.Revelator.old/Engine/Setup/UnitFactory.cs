using System;
using RPGLibrary;

// Unused
namespace Davfalcon.Engine.Setup
{
	public class UnitFactory
	{
		private int attributeBaseline;

		public IUnit MakeUnit(string name, string className, int level)
		{
			Unit unit = new Unit()
			{
				Name = name,
				Class = className,
				Level = level
			};

			foreach (Attributes stat in Enum.GetValues(typeof(Attributes)))
			{
				unit.BaseStats[stat] = attributeBaseline;
			}

			return unit;
		}

		public UnitFactory(int attributeBaseline)
		{
			this.attributeBaseline = attributeBaseline;
		}

		public UnitFactory() : this(UnitStats.BASE_ATTRIBUTE) { }
	}
}
