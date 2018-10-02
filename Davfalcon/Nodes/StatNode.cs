using System;
using System.Collections.Generic;
using Davfalcon.Collections.Generic;

namespace Davfalcon.Nodes
{
	public struct StatNode : IMathNode
	{
		private readonly IStats source;
		private readonly Enum stat;

		public int Value => source?[stat] ?? 0;
		public object Source => source;
		public Type SourceType { get; }
		public string Name { get; }
		public IEnumerable<IMathNode> Children { get; }

		public StatNode(IStats source, Enum stat, string name)
			: this(source, stat, name, typeof(IStats))
		{ }

		public StatNode(IStats source, Enum stat, string name, Type sourceType)
		{
			this.source = source;
			this.stat = stat;

			Name = name;
			SourceType = sourceType;
			Children = new EmptyEnumerable<IMathNode>();
		}

		public static StatNode From<T>(T source, Enum stat, string name) where T : IStats
			=> new StatNode(source, stat, name, typeof(T));

		public static StatNode From<T>(T source, Enum stat) where T : IStats, INameable	
			=> new StatNode(source, stat, source.Name, typeof(T));
	}
}
