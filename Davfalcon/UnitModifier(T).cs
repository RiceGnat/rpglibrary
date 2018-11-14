using System;

namespace Davfalcon
{
	/// <summary>
	/// Abstract base class for unit modifiers.
	/// </summary>
	[Serializable]
	public abstract class UnitModifier<T> : Modifier<T>, IEditableDescription where T : IUnit
	{
		protected class Decorator : IUnit
		{
			private readonly UnitModifier<IUnit> modifier;
			public IUnit Target => modifier.Target;

			string INameable.Name => Target.Name;
			string IUnit.Class => Target.Class;
			int IUnit.Level => Target.Level;
			IStats IStatsContainer.Stats => Target.Stats;
			IStatsDetails IStatsContainer.StatsDetails => Target.StatsDetails;
			IModifierCollection<IUnit> IUnit.Modifiers => Target.Modifiers;

			public Decorator(UnitModifier<IUnit> modifier) => this.modifier = modifier;
		}

		protected abstract T InterfaceUnit { get; }

		public override T AsTargetInterface => InterfaceUnit;
	}
}
