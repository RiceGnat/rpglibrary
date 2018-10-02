using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Davfalcon.Nodes
{
	public class AggregatorNode : NodeEnumerableBase, IAggregatorNode
	{
		private IAggregator aggregator;

		public string Name { get; }
		public int Value => Nodes?.Select(node => node.Value).Aggregate(aggregator.AggregateSeed, aggregator.Aggregate) ?? aggregator.AggregateSeed;

		public IEnumerable<INode> Nodes { get; }

		public AggregatorNode(string name, IEnumerable<INode> values, IAggregator aggregator)
		{
			this.aggregator = aggregator;
			Name = name;
			Nodes = values.ToNewReadOnlyCollectionSafe();
		}

		public AggregatorNode(string name, IEnumerable<INode> values)
			: this(name, values, StatsOperations.Default)
		{ }

		public IAggregatorNode Merge(INode node)
			=> new AggregatorNode(Name, Nodes.Append(node), aggregator);

		public IAggregatorNode Merge(IAggregatorNode node)
			=> new AggregatorNode(Name, Nodes.Union(node.Nodes), aggregator);

		public override string ToString()
			=> $"Aggregator: {Value} {Name}";

		protected override IEnumerator<INode> GetEnumerator()
			=> Nodes.GetEnumerator();
	}
}
