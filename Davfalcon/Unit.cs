using System;
using RPGLibrary;

namespace Davfalcon
{
	[Serializable]
	public class Unit : BasicUnit
	{
		public IUnitModifierStack Equipment { get; protected set; }
		public IUnitModifierStack Buffs { get; protected set; }

		protected override void Initialize()
		{
			BaseStats = new UnitStats(this);
			Modifiers = new UnitModifierStack();
			Equipment = new UnitModifierStack();
			Buffs = new UnitModifierStack();
			Modifiers.Add(Equipment);
			Modifiers.Add(Buffs);
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
