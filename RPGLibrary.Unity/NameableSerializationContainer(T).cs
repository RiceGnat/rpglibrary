using System;
using RPGLibrary;

namespace UnityEngine
{
	[Serializable]
	public class NameableSerializationContainer<T> : SerializationContainer<T> where T : IEditableName, new()
	{
		public override void SerializationPrep()
		{
			obj.Name = name;
		}
	}
}
