using System;
using System.Collections.Generic;

namespace Davfalcon.Nodes
{
	public class ResolverNode : IMathNode
	{
		private readonly IStatsResolver resolver;
		private readonly IMathNode baseValue;
		private readonly IMathNode additions;
		private readonly IMathNode multipliers;

		public int Value => resolver.Calculate(additions.Value, baseValue.Value, multipliers.Value);
		public object Source => this;
		public Type SourceType => typeof(ResolverNode);
		public IEnumerable<IMathNode> Children { get; }
		public string Name { get; }

		public ResolverNode(string name, IMathNode baseValue, IMathNode additions, IMathNode multipliers, IStatsResolver resolver)
		{
			this.resolver = resolver;
			this.baseValue = baseValue;
			this.additions = additions;
			this.multipliers = multipliers;

			Name = name;
			Children = new List<IMathNode>() { baseValue, additions, multipliers }.AsReadOnly();
		}
	}
}
