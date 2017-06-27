using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGLibrary;

namespace RPGLibraryTest
{
	class Program
	{
		static void Main(string[] args)
		{
			BasicUnit baseUnit = new BasicUnit();
			baseUnit.Name = "Test unit";
			baseUnit.BaseStats["ATK"] = 10;

			IUnit unit = new DynamicUnit(baseUnit);

			Console.WriteLine("{0}'s ATK is {1}", unit.Name, unit.Stats["ATK"]);


			UnitStatsModifier mod1 = new UnitStatsModifier();
			mod1.Additions["ATK"] = 5;
			unit.Modifiers.Add(mod1);

			Console.WriteLine("{0}'s ATK is {1}", unit.Name, unit.Stats["ATK"]);


			UnitStatsModifier mod2 = new UnitStatsModifier();
			mod2.Additions["ATK"] = 7;
			mod2.Multiplications["ATK"] = 100;
			unit.Modifiers.Add(mod2);

			Console.WriteLine("{0}'s ATK is {1}", unit.Name, unit.Stats["ATK"]);


			UnitStatsModifier mod3 = new UnitStatsModifier();
			mod3.Additions["ATK"] = 10;
			mod3.Multiplications["ATK"] = -20;
			unit.Modifiers.Add(mod3);

			Console.WriteLine("{0}'s ATK is {1}", unit.Name, unit.Stats["ATK"]);

			Console.ReadKey();
		}
	}
}
