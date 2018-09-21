﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace Davfalcon.Collections.Generic
{
	[Serializable]
	public class ListAdapter<T1, T2> : IList<T2> where T1 : class where T2 : class
	{
		private readonly IList<T1> list;

		T2 IList<T2>.this[int index]
		{
			get => (T2)(object)list[index];
			set => list[index] = value as T1;
		}

		int ICollection<T2>.Count => list.Count;
		bool ICollection<T2>.IsReadOnly => list.IsReadOnly;

		void ICollection<T2>.Add(T2 item)
			=> list.Add(item as T1);

		void ICollection<T2>.Clear()
			=> list.Clear();

		bool ICollection<T2>.Contains(T2 item)
			=> list.Contains(item as T1);

		void ICollection<T2>.CopyTo(T2[] array, int arrayIndex)
			=> list.CopyTo(array as T1[], arrayIndex);

		private IEnumerator<T2> GetEnumerator()
			=> new EnumeratorAdapter(this);

		IEnumerator<T2> IEnumerable<T2>.GetEnumerator()
			=> GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> GetEnumerator();

		int IList<T2>.IndexOf(T2 item)
			=> list.IndexOf(item as T1);

		void IList<T2>.Insert(int index, T2 item)
			=> list.Insert(index, item as T1);

		bool ICollection<T2>.Remove(T2 item)
			=> list.Remove(item as T1);

		void IList<T2>.RemoveAt(int index)
			=> list.RemoveAt(index);

		public ListAdapter(IList<T1> source)
			=> list = source;

		private class EnumeratorAdapter : IEnumerator<T2>
		{
			private readonly IList<T2> list;
			private int index;

			public EnumeratorAdapter(IList<T2> list)
			{
				this.list = list;
				Reset();
			}

			public T2 Current => index < 0 ? default : list[index] as T2;
			object IEnumerator.Current => Current;

			void IDisposable.Dispose() { }

			public bool MoveNext()
			{
				index++;
				return index < list.Count;
			}

			public void Reset()
				=> index = -1;
		}
	}
}
