using System;
using RPGLibrary.Serialization;
using UnityEngine;

namespace Davfalcon.Unity
{
	[Serializable]
	public abstract class SerializationContainer<T> : ObjectContainer, ISerializationCallbackReceiver where T : new()
	{
		[NonSerialized]
		public T obj = new T();
		[NonSerialized]
		private bool deserialized = false;

		[SerializeField]
		private byte[] data;

		public override U GetObjectAs<U>()
		{
			if (!deserialized) OnAfterDeserialize();
			return obj as U;
		}

		public abstract void SerializationPrep();

		public void OnBeforeSerialize()
		{
			SerializationPrep();
			data = Serializer.ConvertToByteArray(obj);
		}

		public void OnAfterDeserialize()
		{
			if (data != null)
			{
				obj = Serializer.ConvertFromByteArray<T>(data);
				data = null;
			}

			deserialized = true;
		}
	}
}
