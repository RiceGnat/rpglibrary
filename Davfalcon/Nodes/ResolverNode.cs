﻿using System.Collections.Generic;

namespace Davfalcon.Nodes
{
	public class ResolverNode : NodeEnumerableBase, IResolverNode
	{
		private readonly IStatsOperations resolver;
		private readonly INode[] nodes = new INode[3];

		public string Name { get; }
		public int Value => resolver.Calculate(Base.Value, Addend.Value, Multiplier.Value);

		public INode Base => nodes[0];
		public INode Addend => nodes[1];
		public INode Multiplier => nodes[2];

		public ResolverNode(string name, INode baseValue, INode addend, INode multiplier, IStatsOperations resolver)
		{
			this.resolver = resolver;
			Name = name;
			nodes[0] = baseValue;
			nodes[1] = addend;
			nodes[2] = multiplier;
		}

		public override string ToString()
			=> $"Resolver: {Value} {Name}";

		protected override IEnumerator<INode> GetEnumerator()
			=> (nodes as IEnumerable<INode>).GetEnumerator();
	}
}
