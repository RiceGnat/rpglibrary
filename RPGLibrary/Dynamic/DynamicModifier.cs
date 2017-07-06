using System;

namespace RPGLibrary.Dynamic
{
	[Serializable]
	public class DynamicModifier : UnitStatsModifier, IDynamicModifier
	{
		public string Name { get; set; }
		public string Description { get; set; }

		public int Duration { get; set; }
		public int Remaining { get; set; }

		public event UnitEventHandler Upkeep;

		public void Tick()
		{
			if (Remaining > 0 || Duration == 0) Upkeep(Target.Modifiers);
			if (Remaining > 0) Remaining--;
		}

		// Resolve Name property ambiguity
		string IUnit.Name { get { return InterfaceUnit.Name; } }
		string IDynamicModifier.Name { get { return Name; } }
	}
}
