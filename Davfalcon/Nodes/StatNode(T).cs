using System;
using System.Collections.Generic;
using Davfalcon.Collections.Generic;
using Davfalcon.Serialization;

namespace Davfalcon.Nodes
{
	public class StatNode<T> : NodeEnumerableBase, IStatNode<T> where T : INameable
	{
		private readonly string name;
		private readonly IStats stats;

		public string Name => Source?.Name ?? name;
		public int Value => stats?[Stat] ?? 0;
		public Enum Stat { get; }
		public T Source { get; }

		public StatNode(string name, IStats stats, Enum stat)
			: this(default(T), stats, stat)
		{
			this.name = name;
		}

		public StatNode(T source, IStats stats, Enum stat)
		{
			name = null;
			this.stats = stats;

			Stat = stat;
			Source = source;
		}

		public static StatNode<TSource> From<TSource>(TSource source, IStats stats, Enum stat) where TSource : INameable
			=> new StatNode<TSource>(source, stats, stat);

		public static StatNode<TSource> From<TSource>(TSource source, Enum stat) where TSource : IStatsHolder, INameable
			=> new StatNode<TSource>(source, source.Stats, stat);

		public override string ToString()
			=> $"Stat: {Value} {Stat} ({Name})";
	}
}
