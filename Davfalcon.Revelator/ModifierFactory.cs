using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davfalcon.Revelator
{
	public class ModifierFactory
	{
		public IEquipment CreateEquipment(string name, Func<Equipment, IEquipment> func)
		{
			Equipment equipment = new Equipment
			{
				Name = name
			};

			return func(equipment);
		}
	}
}
