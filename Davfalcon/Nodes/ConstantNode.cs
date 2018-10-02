using System;
using System.Collections.Generic;
using Davfalcon.Collections.Generic;

namespace Davfalcon.Nodes
{
	[Serializable]
	public struct ConstantNode : IMathNode
	{
		public int Value { get; }
		public object Source { get; }
		public Type SourceType => typeof(int);
		public string Name { get; }
		public IEnumerable<IMathNode> Children { get; }

		public ConstantNode(int value, string name)
		{
			Value = value;
			Name = name;
			Source = value;
			Children = new EmptyEnumerable<IMathNode>();
		}

		public ConstantNode(int value)
			: this(value, "")
		{ }

		public static ConstantNode One { get; } = new ConstantNode(1);
		public static ConstantNode Zero { get; } = new ConstantNode(0);
	}
}
