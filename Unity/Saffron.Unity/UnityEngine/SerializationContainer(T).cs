using System;
using Saffron.Serialization;

namespace UnityEngine
{
	/// <summary>
	/// Stores a C# object in a format serializable by the Unity Engine.
	/// </summary>
	/// <typeparam name="T">The serialized type.</typeparam>
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
