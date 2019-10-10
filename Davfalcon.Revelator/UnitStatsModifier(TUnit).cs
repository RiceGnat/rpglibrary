using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator
{
    public abstract class UnitStatsModifier<TUnit> : Davfalcon.UnitStatsModifier<TUnit> where TUnit : IUnitTemplate<TUnit>
    {
        protected override Func<int, int, int> GetAggregator(Enum type) => (a, b) => a + b;

        protected override int GetAggregatorSeed(Enum type) => 0;

        protected override int Resolver(int baseValue, IReadOnlyDictionary<Enum, int> modifications)
            => (modifications[StatModType.Additive] + baseValue).Scale(modifications[StatModType.Scaling]);
    }
}
