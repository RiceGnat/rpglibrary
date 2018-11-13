using System;

namespace Davfalcon
{
	/// <summary>
	/// Abstract base class for unit modifiers.
	/// </summary>
	[Serializable]
	public abstract class UnitModifier<T> : Modifier<T>, IEditableDescription, IUnit where T : IUnit
	{
		string IUnit.Name => Target.Name;
		string IUnit.Class => Target.Class;
		int IUnit.Level => Target.Level;
		IStats IStatsContainer.Stats => Target.Stats;
		IStatsDetails IStatsContainer.StatsDetails => Target.StatsDetails;
		IModifierCollection<IUnit> IUnit.Modifiers => Target.Modifiers;

		protected abstract T InterfaceUnit { get; }

		public override T AsTargetInterface => InterfaceUnit;
	}
}
