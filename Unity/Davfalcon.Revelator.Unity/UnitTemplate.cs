using UnityEngine;

namespace Davfalcon.Revelator.Unity
{
	[CreateAssetMenu(menuName = "Unit template", fileName = "Unit")]
	public class UnitTemplate : NameableSerializationContainer<Unit>
	{
		public bool attributesExpanded = true;
		public bool equipmentExpanded = true;
		public bool statsExpanded = true;
		public bool inventoryExpanded = true;
	}
}
