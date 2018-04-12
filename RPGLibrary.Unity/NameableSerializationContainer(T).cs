using System;
using RPGLibrary;

namespace UnityEngine
{
	/// <summary>
	/// A serialization container supporting automatic name binding.
	/// </summary>
	/// <typeparam name="T">The serialized type.</typeparam>
	[Serializable]
	public class NameableSerializationContainer<T> : SerializationContainer<T> where T : IEditableName, new()
	{
		public override void SerializationPrep()
		{
			obj.Name = name;
		}
	}
}
