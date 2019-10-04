﻿using System;

namespace Davfalcon
{
    /// <summary>
    /// Exposes basic properties of a unit.
    /// </summary>
    public interface IUnitTemplate<T>
    {
        /// <summary>
        /// Gets the unit's name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the unit's stats.
        /// </summary>
        IStatsProperties Stats { get; }

        /// <summary>
        /// Gets the modifiers attached to the unit.
        /// </summary>
        IModifierStack<T> Modifiers { get; }
    }
}