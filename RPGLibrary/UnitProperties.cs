using System;

namespace RPGLibrary
{
	/// <summary>
	/// Implements storage for a unit's properties.
	/// </summary>
	[Serializable]
	public class UnitProperties : IUnitProperties
	{
		public string Name { get; set; }
		public string Class { get; set; }
		public int Level { get; set; }

		public T GetAs<T>() where T : class, IUnitProperties
		{
			return this as T;
		}
	}
}
