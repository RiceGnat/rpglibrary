using System;
using System.Collections.Generic;

namespace Davfalcon.Collections.Generic
{
	/// <summary>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
	public class ManagedList<T, U> : ManagedList<T> where T : class where U : class
	{
		[NonSerialized]
		private IList<U> readOnly;

		/// <summary>
		/// Gets the read-only collection wrapper for the <see cref="List{T}"/>.
		/// </summary>
		new public IList<U> ReadOnly
		{
			get
			{
				if (readOnly == null)
					readOnly = new ListAdapter<T, U>(AsReadOnly());
				return readOnly;
			}
		}
	}
}
