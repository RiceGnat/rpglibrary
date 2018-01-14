using UnityEngine;

namespace Davfalcon.Unity
{
	[CreateAssetMenu(menuName = "Equipment")]
	public class EquipmentDefinition : NameableSerializationContainer<Equipment>
	{
		public bool statsExpanded = true;
		public bool buffsExpanded = true;

		private void Awake()
		{
			if (obj == null) obj = new Equipment(EquipmentSlot.Armor);
		}
	}
}
