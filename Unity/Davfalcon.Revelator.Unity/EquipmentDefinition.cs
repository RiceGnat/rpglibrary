using UnityEngine;

namespace Davfalcon.Revelator.Unity
{
	[CreateAssetMenu(menuName = "Equipment", fileName = "Equipment")]
	public class EquipmentDefinition : NameableSerializationContainer<Equipment>
	{
		public bool statsExpanded = true;
		public bool buffsExpanded = true;
		public bool effectsExpanded = true;
	}
}
