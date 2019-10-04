using System;

namespace Davfalcon
{
    /// <summary>
    /// Abstract base class for unit modifiers.
    /// </summary>
    [Serializable]
	public abstract class UnitModifier<T> : Modifier<T>, IUnitTemplate<T> where T : IUnitTemplate<T>
    {
        // Default passthrough behavior
        string IUnitTemplate<T>.Name => Target.Name;
        IStatsProperties IUnitTemplate<T>.Stats => Target.Stats;
        IModifierStack<T> IUnitTemplate<T>.Modifiers => Target.Modifiers;
    }
}
