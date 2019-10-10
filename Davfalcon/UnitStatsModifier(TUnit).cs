﻿using System;
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
        [Serializable]
        private class UnitStatsProxy : StatsPrototype, IStatsProperties
        {
            private readonly UnitStatsModifier<TUnit> modifier;

            public UnitStatsProxy(UnitStatsModifier<TUnit> modifier)
            {
                this.modifier = modifier;
            }

            public IStats Base => modifier.GetBaseStats();

            public int GetModificationBase(Enum stat) => modifier.GetModificationBaseStat(stat);

            public IStatNode GetStatNode(Enum stat) => modifier.GetStatNode(stat);

            public override int Get(Enum stat) => GetStatNode(stat).Value;
        }

        private readonly UnitStatsProxy statsProxy;

        public IDictionary<Enum, IStatsEditable> StatModifications { get; } = new Dictionary<Enum, IStatsEditable>();

        IStatsProperties IUnitTemplate<TUnit>.Stats => statsProxy;

        public void AddStatModificationType(Enum type) => StatModifications.Add(type, new StatsMap());

        public IStats GetStatModifications(Enum type) => StatModifications[type];

        protected virtual IStatNode GetStatNode(Enum stat)
        {
            IStatNode targetStatNode = Target.Stats.GetStatNode(stat);
            return new StatNode(stat.ToString(), GetModificationBaseStat(stat), Resolver,
                StatModifications.ToDictionary(kvp => kvp.Key, kvp =>
                {
                    INode<int> prev = targetStatNode.GetModification(kvp.Key);

                    List<INode<int>> mods = new List<INode<int>>();
                    if (prev != null)
                    {
                        mods.AddRange(prev);
                    }

                    mods.Add(new ValueNode<int>(kvp.Value[stat]));

                    return new AggregatorNode<int>(mods, GetAggregator(kvp.Key), GetAggregatorSeed(kvp.Key)) as INode<int>;
                })
            );
        }

        protected IStats GetBaseStats() => Target.Stats.Base;

        protected int GetModificationBaseStat(Enum stat) => Target.Stats.GetModificationBase(stat);

        protected abstract int Resolver(int baseValue, IDictionary<Enum, int> modifications);

        protected abstract Func<int, int, int> GetAggregator(Enum type);

        protected abstract int GetAggregatorSeed(Enum type);

        public override void Bind(TUnit target)
        {
            base.Bind(target);
        }

        public UnitStatsModifier()
        {
            statsProxy = new UnitStatsProxy(this);
        }
    }
}
