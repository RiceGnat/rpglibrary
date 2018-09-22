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
		[NonSerialized]
		private IList<T> readOnly;

		/// <summary>
		/// Gets the read-only collection wrapper for the <see cref="List{T}"/>.
		/// </summary>
		public IList<T> ReadOnly
		{
			get
			{
				if (readOnly == null)
					readOnly = AsReadOnly();
				return readOnly;
			}
		}
	}
}
