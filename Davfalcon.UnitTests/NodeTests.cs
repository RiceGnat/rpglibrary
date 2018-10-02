using Davfalcon.Nodes;
using Davfalcon.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon.UnitTests
{
	[TestClass]
	public class NodeTests
	{
		[TestMethod]
		public void TypeSerialization()
		{
			IMathNode node = new ConstantNode(3, "Test");
			IMathNode clone = node.DeepClone();

			Assert.AreEqual(typeof(int), clone.SourceType);
		}

	}
}
