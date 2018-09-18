using UnityEngine;

namespace Davfalcon.Revelator.Unity
{
	[CreateAssetMenu(menuName = "Usable item", fileName = "Item")]
	public class UsableItemDefinition : NameableSerializationContainer<UsableItem>
	{
		public bool effectsExpanded = true;
	}
}
