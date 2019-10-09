using System;
using System.Collections.Generic;
using Davfalcon.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon.Stats
{
    [TestClass]
    public class StatNodeTests
    {
        private enum ModType
        {
            Add,
            Multiply
        }

        [TestMethod]
        public void ValueResolver()
        {
            IStatNode node = new StatNode("STAT", 11, (b, a) => a[ModType.Add] + b * a[ModType.Multiply], new Dictionary<Enum, INode<int>>() {
                {
                    ModType.Add,
                    new AggregatorNode<int>(new[] {
                        new ValueNode<int>(1),
                        new ValueNode<int>(2)
                    }, (a, b) => a + b)
                },
                {
                    ModType.Multiply,
                    new ValueNode<int>(3)
                }
            });

            Assert.AreEqual(36, node.Value);
        }

        [TestMethod]
        public void BaseValueOnly()
        {
            IStatNode node = new StatNode("STAT", 11);

            Assert.AreEqual(11, node.Base);
            Assert.AreEqual(11, node.Value);
        }
    }
}
