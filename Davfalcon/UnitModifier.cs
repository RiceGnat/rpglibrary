using System;

namespace Davfalcon
{
	/// <summary>
	/// Abstract base class for unit modifiers.
	/// </summary>
	[Serializable]
	public abstract class UnitModifier<T> : Modifier<T>, IUnit, IEditableDescription where T : IUnit
	{
		string IUnit.Name => Target.Name;
		string IUnit.Class => Target.Class;
		int IUnit.Level => Target.Level;
		IStats IStatsContainer.Stats => Target.Stats;
		IStatsDetails IStatsContainer.StatsDetails => Target.StatsDetails;
		IModifierCollection<IUnit> IUnit.Modifiers => Target.Modifiers;

		protected abstract T GetAsTargetInterface();

		public override T AsTargetInterface => GetAsTargetInterface();
	}

	[Serializable]
	internal class UnitModifier : UnitModifier<IUnit>
	{
		protected override IUnit GetAsTargetInterface() => this;
	}
}
