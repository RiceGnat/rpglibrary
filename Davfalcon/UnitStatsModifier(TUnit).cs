using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Nodes;
using Davfalcon.Stats;

namespace Davfalcon
{
    /// <summary>
    /// Abstract base stats for modifiers affecting unit stats.
    /// </summary>
    /// <typeparam name="TUnit">The interface used by the unit's implementation.</typeparam>
    [Serializable]
    public abstract class UnitStatsModifier<TUnit> : UnitModifier<TUnit>, IStatsModifier<TUnit>, IUnitTemplate<TUnit> where TUnit : IUnitTemplate<TUnit>
    {
        private class UnitStatsProxy : StatsPrototype, IStatsProperties
        {
            private readonly TUnit target;
            private readonly UnitStatsModifier<TUnit> modifier;

            public UnitStatsProxy(TUnit target, UnitStatsModifier<TUnit> modifier)
            {
                this.target = target;
                this.modifier = modifier;
            }

            public IStats Base => modifier.GetBaseStats();

            public override int Get(Enum stat) => GetStatNode(stat).Value;

            public IStatNode GetStatNode(Enum stat) => modifier.GetStatNode(stat);
        }

        [NonSerialized]
        private UnitStatsProxy statsProxy;

        public IDictionary<Enum, IStatsEditable> StatModifications { get; } = new Dictionary<Enum, IStatsEditable>();

        IStatsProperties IUnitTemplate<TUnit>.Stats => statsProxy;

        public void AddStatModificationType(Enum type) => StatModifications.Add(type, new StatsMap());

        public IStats GetStatModifications(Enum type) => StatModifications[type];

        protected abstract int Resolver(int baseValue, IDictionary<Enum, int> modifications);
        protected abstract Func<int, int, int> GetAggregator(Enum type);

        protected virtual IStatNode GetStatNode(Enum stat)
            => new StatNode(stat.ToString(), GetBaseStats()[stat], Resolver,
                StatModifications.ToDictionary(kvp => kvp.Key, kvp =>
                {
                    IEnumerable<INode<int>> prevNode = Target.Stats.GetStatNode(stat).GetModification(kvp.Key) ?? Enumerable.Empty<INode<int>>();
                    return kvp.Value[stat] != 0
                        ? AggregatorNode<int>.Append(prevNode, new ValueNode<int>(kvp.Value[stat]), GetAggregator(kvp.Key))
                        : AggregatorNode<int>.Union(prevNode, Enumerable.Empty<INode<int>>(), GetAggregator(kvp.Key));
                }));

        protected IStats GetBaseStats() => Target.Stats.Base;

        public override void Bind(TUnit target)
        {
            base.Bind(target);
            statsProxy = new UnitStatsProxy(Target, this);
        }
    }
}
