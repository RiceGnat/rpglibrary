using System;

namespace Davfalcon
{
    /// <summary>
    /// Abstract base class for unit modifiers.
    /// </summary>
    /// <typeparam name="TUnit">The interface used by the unit's implementation.</typeparam>
    [Serializable]
	public abstract class UnitModifier<TUnit> : Modifier<TUnit>, IUnitTemplate<TUnit> where TUnit : IUnitTemplate<TUnit>
    {
        protected abstract TUnit Self { get; }

        public override TUnit AsModified() => Self;

        // Default passthrough behavior
        string IUnitTemplate<TUnit>.Name => Target.Name;
        IStatsProperties IUnitTemplate<TUnit>.Stats => Target.Stats;
        IModifierStack<TUnit> IUnitTemplate<TUnit>.Modifiers => Target.Modifiers;
    }
}
