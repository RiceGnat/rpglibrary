using System;
using System.Collections.Generic;
using System.Linq;

namespace Davfalcon.Nodes
{
    public class AggregatorNode<T> : NodeBase<T>
    {
        private readonly Func<T, T, T> func;

        public AggregatorNode(IEnumerable<INode<T>> nodes, Func<T, T, T> func)
        {
            this.func = func ?? throw new ArgumentNullException();
            if (nodes == null) throw new ArgumentNullException();

            Nodes = nodes.ToList();
            Value = nodes
                .Select(node => node.Value)
                .Aggregate(func);
        }

        public static INode<T> Append(IEnumerable<INode<T>> nodes, INode<T> node, Func<T, T, T> func)
            => new AggregatorNode<T>(nodes.Append(node), func);

        public static INode<T> Union(IEnumerable<INode<T>> nodes, IEnumerable<INode<T>> otherNodes, Func<T, T, T> func)
            => new AggregatorNode<T>(nodes.Union(otherNodes), func);
    }
}
