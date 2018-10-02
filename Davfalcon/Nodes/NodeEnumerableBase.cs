using System;
using System.Collections;
using System.Collections.Generic;

namespace Davfalcon.Nodes
{
	public abstract class NodeEnumerableBase : IEnumerable<INode>
	{
		private class DummyEnumerator : IEnumerator<INode>
		{
			INode IEnumerator<INode>.Current => null;
			object IEnumerator.Current => null;
			void IDisposable.Dispose() { }
			bool IEnumerator.MoveNext() => false;
			void IEnumerator.Reset() { }
		}

		protected virtual IEnumerator<INode> GetEnumerator()
			=> new DummyEnumerator();

		IEnumerator<INode> IEnumerable<INode>.GetEnumerator()
			=> GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> GetEnumerator();
	}
}
