using System;
using System.Runtime.Serialization;
using Davfalcon.Stats;

namespace Davfalcon
{
    /// <summary>
    /// Implements basic unit functionality.
    /// </summary>
    /// <typeparam name="TUnit">The interface used by the unit's implementation.</typeparam>
    [Serializable]
    public abstract class UnitTemplate<TUnit> : IUnitTemplate<TUnit> where TUnit : IUnitTemplate<TUnit>
    {
        protected interface IUnitStats : IStatsEditable, IStatsProperties { }

        protected class UnitStats : StatsMap, IUnitStats
        {
            public IStats Base => this;

            public IStatNode GetStatNode(Enum stat) => new StatNode(stat.ToString(), this[stat]);
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public IStatsEditable BaseStats { get; private set; }
        public IStatsProperties Stats { get; private set; }

        public IModifierStack<TUnit> Modifiers { get; private set; }

        protected abstract TUnit Self { get; }

        protected virtual IUnitStats InitializeUnitStats() => new UnitStats();

        protected virtual IModifierStack<TUnit> InitializeModifierStack() => new ModifierStack<TUnit>();

        protected virtual void Setup()
        {
            IUnitStats stats = InitializeUnitStats();
            BaseStats = stats;
            Stats = stats;

            Modifiers = InitializeModifierStack();
        }

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
            Setup();
            Link();
        }
    }
}
