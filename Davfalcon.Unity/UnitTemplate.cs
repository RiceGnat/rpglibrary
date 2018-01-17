using UnityEngine;

namespace Davfalcon.Unity
{
	[CreateAssetMenu(menuName = "Unit template", fileName = "Unit")]
	public class UnitTemplate : NameableSerializationContainer<Unit>
	{
		public bool attributesExpanded = true;
		public bool equipmentExpanded = true;
		public bool statsExpanded = true;
	}
}
