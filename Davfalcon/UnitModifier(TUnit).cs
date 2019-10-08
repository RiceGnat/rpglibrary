using System;

namespace Davfalcon
{
    /// <summary>
    /// Abstract base class for unit modifiers.
    /// </summary>
    [Serializable]
	public abstract class UnitModifier<TUnit> : Modifier<TUnit>, IUnitTemplate<TUnit> where TUnit : IUnitTemplate<TUnit>
    {
        // Default passthrough behavior
        string IUnitTemplate<TUnit>.Name => Target.Name;
        IStatsProperties IUnitTemplate<TUnit>.Stats => Target.Stats;
        IModifierStack<TUnit> IUnitTemplate<TUnit>.Modifiers => Target.Modifiers;
    }
}
