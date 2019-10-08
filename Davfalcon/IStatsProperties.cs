using System;
using Davfalcon.Stats;

namespace Davfalcon
{
    public interface IStatsProperties : IStats
    {
        IStats Base { get; }

        IStatNode GetStatNode(Enum stat);
        IStatNode GetStatNode(string stat);
    }
}
