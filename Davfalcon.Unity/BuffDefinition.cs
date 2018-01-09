using System;
using RPGLibrary.Serialization;
using UnityEngine;

namespace Davfalcon.Unity
{
	[CreateAssetMenu(menuName = "Buff")]
	public class BuffDefinition : ScriptableObject, ISerializationCallbackReceiver
	{
		[NonSerialized]
		public Buff buff = new Buff();

		[SerializeField]
		private byte[] data;

		public void OnBeforeSerialize()
		{
			buff.Name = name;
			data = Serializer.ConvertToByteArray(buff);
		}

		public void OnAfterDeserialize()
		{
			if (data != null)
				buff = Serializer.ConvertFromByteArray<Buff>(data);
		}
	}
}
