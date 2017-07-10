using RPGLibrary;

namespace Davfalcon
{
	public class Unit : BasicUnit
	{
		protected override void Initialize()
		{
			BaseStats = new UnitStats();
			Properties = new UnitProperties(this);
			Modifiers = new UnitModifierStack();
		}
	}
}
