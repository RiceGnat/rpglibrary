using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGLibrary;
using RPGLibrary.Collections;
using RPGLibrary.Dynamic;
using RPGLibrary.Items;
using RPGLibrary.Serialization;

namespace RPGLibraryTest
{
	class Program
	{
		static void Main(string[] args)
		{
			BasicUnit baseUnit = new BasicUnit();
			baseUnit.Name = "Test unit 1";
			baseUnit.BaseStats["ATK"] = 10;
			baseUnit.BaseStats["DEF"] = 10;

			IUnit unit = new DynamicUnit(baseUnit);
			BasicUnit unit2 = Serializer.DeepClone<BasicUnit>(baseUnit);
			BasicUnit unit3;
			unit2.Name = "Test unit 2";

			PrintUnit(unit);

			//DynamicModifier d = new DynamicModifier();
			//d.Name = "Dynamic A";
			//d.Multiplications["ATK"] = 50;
			//d.Duration = 4;
			//d.Remaining = 4;
			//d.Upkeep += PrintUnit;

			//unit.Modifiers.Add(d);

			Catalog<Equipment> c = new Catalog<Equipment>();

			Equipment e = new Equipment();
			e.Name = "Equipment A";
			e.Additions["ATK"] = 10;
			e.Additions["DEF"] = -2;

			unit.Modifiers.Add(e);
			PrintEquipment(e);
			PrintUnit(unit);
			c.Add(e);

			e = new Equipment();
			e.Name = "Equipment B";
			e.Additions["ATK"] = 4;
			e.Additions["DEF"] = 4;

			unit.Modifiers.Add(e);
			PrintEquipment(e);
			PrintUnit(unit);
			c.Add(e);

			unit.Modifiers.Remove(e);
			PrintUnit(unit);

			PrintEquipment(c.GetCopy(0));
			PrintEquipment(c.GetCopy(1));
			
			//while (d.Remaining > 0)
			//{
			//	Console.Write("{0}: ", d.Remaining);
			//	d.Tick();
			//	Console.ReadKey();
			//}

			PrintUnit(unit);
			PrintUnit(unit2.Modifiers);

			unit3 = Serializer.DeepClone<BasicUnit>(baseUnit);
			unit3.Name = "Test unit 3";
			unit3.Modifiers.Add(c.GetCopy(1));
			PrintUnit(unit3.Modifiers);

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
