using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RPGLibrary.Collections.Generic
{
	/// <summary>
	/// Represents a list that can be circularly rotated in one direction.
	/// </summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	[Serializable]
	public class CircularLinkedList<T> : ICircularLinkedList<T>
	{
		private List<T> list = new List<T>();
		private int head = 0;

		private int GetActualIndexFromOffset(int offsetIndex)
		{
			return (head + offsetIndex) % Count;
		}

		private int GetOffsetIndexFromActual(int actualIndex)
		{
			return (actualIndex - head) % Count;
		}

		private void ThrowIfIndexOutOfRange(int index)
		{
			if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException();
		}

		public T Current
		{
			get { return list[head]; }
		}

		public void Rotate()
		{
			Rotate(1);
		}

		public void Rotate(int steps)
		{
			if (Count > 1)
				head = GetActualIndexFromOffset(steps);
		}

		public int IndexOf(T item)
		{
			int index = list.IndexOf(item, head);
			if (head > 0 && index < 0)
			{
				index = list.IndexOf(item, 0, head);
			}
			return index < 0 ? index : GetOffsetIndexFromActual(index);
		}

		public void Insert(int index, T item)
		{
			ThrowIfIndexOutOfRange(index);
			int actual = GetActualIndexFromOffset(index);
			list.Insert(actual, item);
			if (actual < head) head++;
		}

		public void RemoveAt(int index)
		{
			ThrowIfIndexOutOfRange(index);
			int actual = GetActualIndexFromOffset(index);
			list.RemoveAt(actual);
			if (actual < head) head--;
		}

		public T this[int index]
		{
			get
			{
				ThrowIfIndexOutOfRange(index);
				return list[GetActualIndexFromOffset(index)];
			}
			set
			{
				ThrowIfIndexOutOfRange(index);
				list[GetActualIndexFromOffset(index)] = value;
			}
		}

		public void Add(T item)
		{
			list.Insert(head, item);
			head++;
		}

		public void Clear()
		{
			list.Clear();
			head = 0;
		}

		public bool Contains(T item)
		{
			return list.Contains(item);
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			for (int i = 0; i < Count; i++)
			{
				array[arrayIndex + i] = this[i];
			}
		}

		public int Count
		{
			get { return list.Count; }
		}

		bool ICollection<T>.IsReadOnly
		{
			get { return false; }
		}

		public bool Remove(T item)
		{
			int index = list.IndexOf(item);
			if (index >= 0)
			{
				list.RemoveAt(index);
				if (index < head) head--;
				return true;
			}
			else
			{
				return false;
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new CLLEnumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public ReadOnlyCollection<T> AsReadOnly()
		{
			return new ReadOnlyCollection<T>(this);
		}

		public CircularLinkedList() { }

		public CircularLinkedList(T item)
		{
			list.Add(item);
		}

		public CircularLinkedList(IEnumerable<T> items)
		{
			list = new List<T>();
			foreach (T item in items)
			{
				list.Add(item);
			}
		}

		public static CircularLinkedList<T> WrapList(List<T> list)
		{
			CircularLinkedList<T> cll = new CircularLinkedList<T>();
			cll.list = list;
			return cll;
		}

		public class CLLEnumerator : IEnumerator<T>
		{
			private CircularLinkedList<T> list;
			private int curIndex;

			public CLLEnumerator(CircularLinkedList<T> list)
			{
				this.list = list;
				Reset();
			}

			public T Current
			{
				get { return curIndex < 0 ? default(T) : list[curIndex]; }
			}

			void IDisposable.Dispose() { }

			object IEnumerator.Current
			{
				get { return Current; }
			}

			public bool MoveNext()
			{
				return curIndex++ < list.Count;
			}

			public void Reset()
			{
				curIndex = -1;
			}
		}
	}
}
