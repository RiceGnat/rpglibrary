using System;
using RPGLibrary;

namespace Davfalcon
{
	[Serializable]
	public class Unit : BasicUnit
	{
		protected override void Initialize()
		{
			BaseStats = new UnitStats(this);
			Modifiers = new UnitModifierStack();
			Properties = new UnitProperties();
		}

		protected override void Link()
		{
			base.Link();
			(BaseStats as UnitStats).Bind(this);
			(Properties as UnitProperties).Bind(this);
		}
	}
}
