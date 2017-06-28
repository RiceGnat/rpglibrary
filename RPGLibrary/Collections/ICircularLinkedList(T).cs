using System.Collections.Generic;

namespace RPGLibrary.Collections.Generic
{
	public interface ICircularLinkedList<T> : IList<T>
	{
		T Current { get; }
		void Rotate();
	}
}
