using System;
using RPGLibrary;
using Davfalcon;

namespace DavfalconUnitTests
{
	static class TestObjects
	{
		public static IUnit GenerateBaselineUnit()
		{
			Unit unit = new Unit
			{
				Name = "Test unit",
				Class = "Class",
				Level = 1
			};
			
			foreach (Attributes stat in Enum.GetValues(typeof(Attributes)))
			{
				unit.BaseStats[stat] = 10;
			}

			unit.BaseStats[Attributes.STR] = 15;
			unit.BaseStats[Attributes.VIT] = 15;

			return unit;
		}
	}
}
