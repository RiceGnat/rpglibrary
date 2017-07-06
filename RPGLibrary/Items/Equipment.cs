using System;

namespace RPGLibrary.Items
{
	[Serializable]
	public class Equipment : UnitStatsModifier, IEquipment
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public bool IsConsumable { get { return false; } }

		IStats IEquipment.Additions { get { return Additions; } }
		IStats IEquipment.Multiplications { get { return Multiplications; } }

		// Resolve Name property ambiguity
		string IUnit.Name { get { return InterfaceUnit.Name; } }
		string IEquipment.Name { get { return Name; } }
	}
}
