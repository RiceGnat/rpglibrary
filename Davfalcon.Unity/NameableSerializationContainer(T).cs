using System;
using RPGLibrary;

namespace Davfalcon.Unity
{
	[Serializable]
	public class NameableSerializationContainer<T> : SerializationContainer<T> where T : IEditableName
	{
		public override void SerializationPrep()
		{
			obj.Name = name;
		}
	}
}
