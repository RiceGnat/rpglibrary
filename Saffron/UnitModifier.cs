using System;

namespace Saffron
{
	/// <summary>
	/// Abstract base class for unit modifiers.
	/// </summary>
	[Serializable]
	public abstract class UnitModifier : IUnitModifier, IEditableDescription
	{
		public string Name { get; set; }
		public string Description { get; set; }

		[NonSerialized]
		private IUnit target;
		protected virtual IUnit InterfaceUnit => Target;
		public IUnit Target => target;

		public virtual void Bind(IUnit target) => this.target = target;

		// Resolve Name property ambiguity
		string IUnitModifier.Name => Name;
		string INameable.Name => Name;

		#region IUnit implementation
		string IUnit.Name => InterfaceUnit.Name;
		string IUnit.Class => InterfaceUnit.Class;
		int IUnit.Level => InterfaceUnit.Level;
		IStats IUnit.Stats => InterfaceUnit.Stats;
		IStatsPackage IUnit.StatsDetails => InterfaceUnit.StatsDetails;
		IUnitModifierStack IUnit.Modifiers => InterfaceUnit.Modifiers;
		#endregion
	}
}
