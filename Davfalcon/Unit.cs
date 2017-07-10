using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Davfalcon
{
	public class Unit : RPGLibrary.BasicUnit
	{
		protected override void Initialize()
		{
			base.Initialize();
			BaseStats = new LinkedStats();
			Properties = new UnitProperties();
		}
	}
}
