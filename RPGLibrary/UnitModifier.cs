using System;

namespace RPGLibrary
{
	[Serializable]
	public abstract class UnitModifier : IUnitModifier
	{
		[NonSerialized]
		private IUnit baseUnit;

		protected virtual IUnit InterfaceUnit { get { return Base; } }

		public uint ID { get; set; }

		public IUnit Base
		{
			get { return baseUnit; }
		}

		public virtual void Bind(IUnit target)
		{
			baseUnit = target;
		}

		#region IUnit
		uint IUnit.ID { get { return InterfaceUnit.ID; } }
		string IUnit.Name { get { return InterfaceUnit.Name; } }
		string IUnit.Class { get { return InterfaceUnit.Class; } }
		int IUnit.Level { get { return InterfaceUnit.Level; } }
		IStats IUnit.Stats { get { return InterfaceUnit.Stats; } }
		IStatsPackage IUnit.StatsDetails { get { return InterfaceUnit.StatsDetails; } }
		IUnitModifierStack IUnit.Modifiers { get { return InterfaceUnit.Modifiers; } }
		IUnitProperties IUnit.Properties { get { return InterfaceUnit.Properties; } }
		#endregion
	}
}
