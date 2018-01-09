using System;
using RPGLibrary.Serialization;
using UnityEngine;

namespace Davfalcon.Unity
{
	[CreateAssetMenu(menuName = "Equipment")]
	public class EquipmentDefinition : ScriptableObject, ISerializationCallbackReceiver
	{
		[NonSerialized]
		public Equipment equipment = new Equipment(EquipmentSlot.Armor);

		[SerializeField]
		private byte[] data;

		public void OnBeforeSerialize()
		{
			equipment.Name = name;
			data = Serializer.ConvertToByteArray(equipment);
		}

		public void OnAfterDeserialize()
		{
			if (data != null)
				equipment = Serializer.ConvertFromByteArray<Equipment>(data);
		}
	}
}
