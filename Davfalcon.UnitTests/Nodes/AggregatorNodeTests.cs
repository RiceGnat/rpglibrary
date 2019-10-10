using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon.Nodes
{
    [TestClass]
    public class AggregatorNodeTests
    {
        private readonly Func<int, int, int> add = (result, value) => result + value;
        private INode<int> node;

        [TestInitialize]
        public void Setup()
        {
            node = new AggregatorNode<int>(new[] {
                new ValueNode<int>(1),
                new ValueNode<int>(2),
                new ValueNode<int>(3)
            }, add);
        }

        [TestMethod]
        public void Value()
        {
            Console.WriteLine(node.Graph());
            Assert.AreEqual(6, node.Value);
        }

        [TestMethod]
        public void Enumeration()
        {
            int sum = 0;
            foreach (INode<int> n in node)
            {
                sum += n.Value;
            }
            Console.WriteLine(node.Graph());
            Assert.AreEqual(6, sum);
        }

        [TestMethod]
        public void Nesting()
        {
            INode<int> newNode = new AggregatorNode<int>(node.Append(new AggregatorNode<int>(new[] {
                new ValueNode<int>(4),
                new ValueNode<int>(5)
            }, add)), add);
            Console.WriteLine(newNode.Graph());
            Assert.AreEqual(15, newNode.Value);
            Assert.AreEqual(4, newNode.Count());
        }

        [TestMethod]
        public void Seed()
        {
            INode<int> node = new AggregatorNode<int>(new[] {
                new ValueNode<int>(0),
                new ValueNode<int>(0)
            }, add, 1);

            Assert.AreEqual(1, node.Value);
        }
    }
}
