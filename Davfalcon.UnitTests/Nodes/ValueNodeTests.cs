using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon.Nodes
{
    [TestClass]
    public class ValueNodeTests
    {
        [TestMethod]
        public void Value()
        {
            INode<int> node = new ValueNode<int>(11);

            Assert.AreEqual(11, node.Value);
        }
    }
}
