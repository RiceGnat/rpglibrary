using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGLibrary;
using RPGLibrary.Items;

namespace RPGLibraryTest
{
	class Program
	{
		static void Main(string[] args)
		{
			BasicUnit baseUnit = new BasicUnit();
			baseUnit.Name = "Test unit";
			baseUnit.BaseStats["ATK"] = 10;
			baseUnit.BaseStats["DEF"] = 10;

			IUnit unit = new DynamicUnit(baseUnit);

			PrintUnit(unit);

			Equipment e = new Equipment();
			e.Name = "Equipment A";
			e.Additions["ATK"] = 10;
			e.Additions["DEF"] = -2;

			unit.Modifiers.Add(e);

			PrintEquipment(e);
			PrintUnit(unit);

			Console.ReadKey();
		}

		static void PrintUnit(IUnit unit)
		{
			Console.WriteLine("{0}'s stats are {1}/{2}", unit.Name, unit.Stats["ATK"], unit.Stats["DEF"]);
		}

		static void PrintEquipment(IEquipment equip)
		{
			Console.WriteLine("{0} grants {1:+#;-#;+0}/{2:+#;-#;+0}", equip.Name, equip.Additions["ATK"], equip.Additions["DEF"]);
		}
	}
}
