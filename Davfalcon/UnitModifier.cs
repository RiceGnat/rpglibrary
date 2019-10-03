using System;

namespace Davfalcon
{
    /// <summary>
    /// Abstract base class for unit modifiers.
    /// </summary>
    [Serializable]
	public abstract class UnitModifier : Modifier<IUnit>, IUnit
	{
        /// <summary>
        /// Gets an <see cref="IUnit"/> that represents the modified unit.
        /// </summary>
        /// <returns>The modified unit as an <see cref="IUnit"/>.</returns>
        public override IUnit AsModified() => this;

        // Default passthrough behavior
        string IUnit.Name => Target.Name;
        string IUnit.Class => Target.Class;
        int IUnit.Level => Target.Level;
        IStats IUnit.Stats => Target.Stats;
        IStatsPackage IUnit.StatsDetails => Target.StatsDetails;
        IModifierStack<IUnit> IUnit.Modifiers => Target.Modifiers;
    }
}
