using System;

namespace RPGLibrary
{
	/// <summary>
	/// Abstract base class for unit modifiers.
	/// </summary>
	[Serializable]
	public abstract class UnitModifier : IUnitModifier
	{
		[NonSerialized]
		private IUnit target;

		protected virtual IUnit InterfaceUnit { get { return Target; } }

		public IUnit Target
		{
			get { return target; }
		}

		public virtual void Bind(IUnit target)
		{
			this.target = target;
		}

		#region IUnit
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
