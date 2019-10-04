using System;
using System.Runtime.Serialization;

namespace Davfalcon
{
    /// <summary>
    /// Implements basic unit functionality.
    /// </summary>
    [Serializable]
    public abstract class UnitTemplate<T> : IUnitTemplate<T> where T : IUnit
    {
        private class UnitStats : StatsMap, IStatsProperties
        {
            public IStats Base
            {
                get { return this; }
            }

            public IStats Additions
            {
                get { return StatsConstant.Zero; }
            }

            public IStats Multiplications
            {
                get { return StatsConstant.Zero; }
            }
        }

        private readonly UnitStats stats = new UnitStats();

        public string Name { get; set; }
        public string Description { get; set; }

        public IStatsProperties Stats => stats;

        public IModifierStack<T> Modifiers { get; } = new ModifierStack<T>();

        /// <summary>
        /// Gets an editable version of the unit's base stats.
        /// </summary>
        public IEditableStats BaseStats => stats;

        protected abstract T Self { get; }

        /// <summary>
        /// Set internal object references
        /// </summary>
        protected virtual void Link()
        {
            // This will initiate the modifier rebinding calls
            Modifiers.Bind(Self);
        }

        [OnDeserialized]
        private void Rebind(StreamingContext context)
        {
            // Reset object references after deserialization
            Link();
        }

        protected UnitTemplate()
        {
            Link();
        }
    }
}
