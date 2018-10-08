using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Nodes;

namespace Davfalcon.Revelator.Combat
{
	public class DamageNode : NodeEnumerableBase, IDamageNode
	{
		private readonly IStatsOperations resolver;
		private readonly IList<INode> nodes = new List<INode>(3);

		public string Name => Source.Name;
		public int Value => resolver.Calculate(Base.Value, Addend?.Value ?? 0, Multiplier?.Value ?? resolver.AggregateSeed);

		public IUnit Unit { get; }
		public IDamageSource Source { get; }
		public INode Base { get; }
		public INode Addend { get; }
		public INode Multiplier { get; }

		public DamageNode(IDamageSource source, IUnit unit, IEnumerable<Enum> scalingStats, IStatsOperations resolver)
		{
			this.resolver = resolver;

			Unit = unit;
			Source = source;
			scalingStats = scalingStats.ToNewReadOnlyCollectionSafe();

			Base = new ConstantNode("Base damage", Source.BaseDamage);
			Addend = Source.BonusDamageStat != null ? Unit.StatsDetails.GetStatNode(Source.BonusDamageStat) : null;

			if (scalingStats.Count() > 1)
			{
				List<INode> nodes = new List<INode>();
				foreach (Enum stat in scalingStats)
				{
					nodes.Add(Unit.StatsDetails.GetStatNode(stat));
				}
				Multiplier = new AggregatorNode("Damage scaling", nodes, resolver);
			}
			else if (scalingStats.Count() == 1)
			{
				Multiplier = Unit.StatsDetails.GetStatNode(scalingStats.First());
			}

			nodes.Add(Base);
			if (Addend != null) nodes.Add(Addend);
			if (Multiplier != null) nodes.Add(Multiplier);
		}

		public override string ToString()
			=> $"Damage: {Value} {String.Join(" ", Source.DamageTypes)} from {Name} ({Unit.Name})";

		protected override IEnumerator<INode> GetEnumerator()
			=> (nodes as IEnumerable<INode>).GetEnumerator();
	}
}
