namespace RPGLibrary
{
	public class DynamicUnit : UnitModifier
	{
		protected override IUnit InterfaceUnit
		{
			get
			{
				return Base.Modifiers;
			}
		}

		public DynamicUnit(IUnit unit) {
			Bind(unit);
		}
	}
}
