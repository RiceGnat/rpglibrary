using System;
using Davfalcon.Nodes;

namespace Davfalcon.Stats
{
    public class StatNode : NodeBase<int>, IStatNode
    {
        public string Name { get; }
        public int Base { get; }

        public INode<int> Additions { get; }

        public INode<int> Multipliers { get; }

        public StatNode(string name, int baseValue, INode<int> additions, INode<int> multipliers, Func<int, int, int> resolver)
        {
            Name = name;
            Base = baseValue;
            Additions = additions;
            Multipliers = multipliers;

            Nodes = new[] { Additions, Multipliers };
            Value = resolver(Additions.Value, Multipliers.Value);
        }
    }
}
