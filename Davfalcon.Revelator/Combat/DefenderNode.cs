using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Nodes;

namespace Davfalcon.Revelator.Combat
{
	public class DefenderNode : NodeEnumerableBase, IResolverNode
	{
		private readonly IStatsOperations resolver;
		private readonly IList<INode> nodes = new List<INode>(2);

		public string Name => Base.Name;
		public int Value { get; }

		public IUnit Defender { get; }
		public IEnumerable<Enum> DefensiveStats { get; }
		public INode Base { get; }
		public INode Addend => null;
		public INode Multiplier { get; }

		public DefenderNode(IUnit defender, INode incomingDamage, IEnumerable<Enum> defensiveStats, IStatsOperations resolver)
		{
			this.resolver = resolver;

			Defender = defender;
			DefensiveStats = defensiveStats.ToNewReadOnlyCollectionSafe();

			Base = incomingDamage;

			if (DefensiveStats.Count() > 1)
			{
				List<INode> nodes = new List<INode>();
				foreach (Enum stat in DefensiveStats)
				{
					nodes.Add(Defender.StatsDetails.GetStatNode(stat));
				}
				Multiplier = new AggregatorNode("Defense", nodes, resolver);
			}
			else if (DefensiveStats.Count() == 1)
			{
				Multiplier = Defender.StatsDetails.GetStatNode(DefensiveStats.First());
			}

			nodes.Add(Base);
			if (Multiplier != null) nodes.Add(Multiplier);

			Value = resolver.ScaleInverse(Base.Value, Multiplier?.Value ?? resolver.AggregateSeed);
		}

		public override string ToString()
			=> $"Defense: {Value} reduced damage from {Name}";

		protected override IEnumerator<INode> GetEnumerator()
			=> (nodes as IEnumerable<INode>).GetEnumerator();
	}
}
