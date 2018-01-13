using System;
using RPGLibrary.Serialization;
using UnityEngine;

namespace Davfalcon.Unity
{
	public abstract class SerializationContainer<T> : ScriptableObject, ISerializationCallbackReceiver
	{
		[NonSerialized]
		public T obj;

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
