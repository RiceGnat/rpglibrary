using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Davfalcon.Collections.Generic
{
	public sealed class EmptyEnumerable<T> : IEnumerable<T>
	{
		private static readonly DummyEnumerator dummy = new DummyEnumerator();

		IEnumerator<T> IEnumerable<T>.GetEnumerator() => dummy;
		IEnumerator IEnumerable.GetEnumerator() => dummy;

		private class DummyEnumerator : IEnumerator<T>
		{
			T IEnumerator<T>.Current => default;
			object IEnumerator.Current => default(T);
			void IDisposable.Dispose() { }
			bool IEnumerator.MoveNext() => false;
			void IEnumerator.Reset() { }
		}
	}
}
