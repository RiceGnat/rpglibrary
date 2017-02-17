using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGLibrary
{
	public interface IUnitModifier : IUnit
	{
		IUnit Base { get; }
		void Bind(IUnit target);
	}
}
