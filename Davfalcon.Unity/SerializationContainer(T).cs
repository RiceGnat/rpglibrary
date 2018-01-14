using System;
using RPGLibrary.Serialization;
using UnityEngine;

namespace Davfalcon.Unity
{
	[Serializable]
	public abstract class SerializationContainer<T> : ScriptableObject, ISerializationCallbackReceiver where T : new()
	{
		[NonSerialized]
		public T obj = new T();

		[SerializeField]
		private byte[] data;

		public abstract void SerializationPrep();

		public void OnBeforeSerialize()
		{
			SerializationPrep();
			data = Serializer.ConvertToByteArray(obj);
		}

		public void OnAfterDeserialize()
		{
			if (data != null)
				obj = Serializer.ConvertFromByteArray<T>(data);
		}
	}
}
