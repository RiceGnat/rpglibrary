using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Nodes;

namespace Davfalcon.Stats
{
    public class StatNode : NodeBase<int>, IStatNode
    {
        private readonly Dictionary<Enum, INode<int>> mods;

        public string Name { get; }
        public int Base { get; }
        public INode<int> GetModification(Enum type) => mods != null && mods.ContainsKey(type) ? mods[type] : null;

        public StatNode(string name, int baseValue, Func<int, IDictionary<Enum, int>, int> resolver, IDictionary<Enum, INode<int>> modifications)
        {
            Name = name;
            Base = baseValue;

            mods = new Dictionary<Enum, INode<int>>(modifications);

            Nodes = mods.Values;
            Value = resolver(baseValue, mods.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Value));
        }

        public StatNode(string name, int baseValue)
        {
            Name = name;
            Base = baseValue;
            Value = Base;
        }
    }
}
