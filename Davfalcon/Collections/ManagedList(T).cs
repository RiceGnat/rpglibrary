using System;
using System.Collections.Generic;

namespace Davfalcon.Collections.Generic
{
	/// <summary>
	/// Convenience class to avoid repeated calls to <see cref="List{T}.AsReadOnly"/>.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class ManagedList<T> : List<T>
	{
		/// <summary>
		/// Gets the read-only collection wrapper for the <see cref="List{T}"/>.
		/// </summary>
		public IList<T> ReadOnly { get; protected set; }

		/// <summary>
		/// Initializes a new <see cref="ManagedList{T}"/>. Equivalent to <see cref="List{T}"/> constructor.
		/// </summary>
		public ManagedList()
		{
			ReadOnly = AsReadOnly();
		}
	}
}
