using System;
using System.Collections.Generic;
using System.Linq;

namespace Davfalcon.Nodes
{
	public struct AggregateNode : IMathNode
	{
		private IAggregator aggregator;

		public int Value => Children?.Select(node => node.Value).Aggregate(aggregator.AggregateSeed, aggregator.Aggregate) ?? aggregator.AggregateSeed;
		public object Source => this;
		public Type SourceType => typeof(AggregateNode);
		public IEnumerable<IMathNode> Children { get; }
		public string Name { get; }

		public AggregateNode(string name, IEnumerable<IMathNode> values, IAggregator aggregator)
		{
			this.aggregator = aggregator;

			Name = name;
			Children = values.ToNewReadOnlyCollectionSafe();
		}

		public AggregateNode(string name, IEnumerable<IMathNode> values)
			: this(name, values, StatsResolver.Default)
		{ }
	}
}
