using System;

namespace RPGLibrary.Dynamic
{
	// Might not be needed!
	[Serializable]
	public class DynamicUnit : UnitModifier
	{
		protected override IUnit InterfaceUnit
		{
			get
			{
				return Target.Modifiers;
			}
		}

		public DynamicUnit(IUnit unit) {
			Bind(unit);
		}
	}
}
