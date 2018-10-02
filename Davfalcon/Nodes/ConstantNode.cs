using System;

namespace Davfalcon.Nodes
{
	[Serializable]
	public class ConstantNode : NodeEnumerableBase, INode
	{
		public string Name { get; }
		public int Value { get; }

		public ConstantNode(string name, int value)
		{
			Name = name;
			Value = value;
		}

		public ConstantNode(int value)
			: this("", value)
		{ }

		public static ConstantNode One { get; } = new ConstantNode(1);
		public static ConstantNode Zero { get; } = new ConstantNode(0);

		public override string ToString()
			=> $"Constant: {Value} {Name}";
	}
}
