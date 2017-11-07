using System.Collections.Generic;

namespace RPGLibrary.Collections.Generic
{
	/// <summary>
	/// Represents a list of objects with a head element that can be circularly rotated.
	/// </summary>
	/// <typeparam name="T">The type of elements in the list.</typeparam>
	public interface ICircularLinkedList<T> : IList<T>
	{
		T Current { get; }
		void Rotate();
	}
}
