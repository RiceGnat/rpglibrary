using UnityEngine;

namespace Davfalcon.Unity
{
	[CreateAssetMenu(menuName = "Equipment")]
	public class EquipmentDefinition : SerializationContainer<Equipment>
	{
		public bool statsExpanded = true;
		public bool buffsExpanded = true;

		private void Awake()
		{
			obj = new Equipment(EquipmentSlot.Armor);
		}

		public override void SerializationPrep()
		{
			obj.Name = name;
		}
	}
}
