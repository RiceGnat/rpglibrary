using UnityEngine;

namespace Davfalcon.Unity
{
	[CreateAssetMenu(menuName = "Equipment")]
	public class EquipmentDefinition : NameableSerializationContainer<Equipment>
	{
		public bool statsExpanded = true;
		public bool buffsExpanded = true;
		public bool effectsExpanded = true;
	}
}
