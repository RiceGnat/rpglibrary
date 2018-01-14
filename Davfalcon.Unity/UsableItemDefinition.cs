using UnityEngine;

namespace Davfalcon.Unity
{
	[CreateAssetMenu(menuName = "Usable Item")]
	public class UsableItemDefinition : NameableSerializationContainer<UsableItem>
	{
		public bool effectsExpanded = true;
	}
}
