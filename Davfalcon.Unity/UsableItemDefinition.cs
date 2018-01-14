using UnityEngine;

namespace Davfalcon.Unity
{
	[CreateAssetMenu(menuName = "Usable Item")]
	public class UsableItemDefinition : NameableSerializationContainer<UsableItem>
	{
		public bool effectsExpanded = true;

		private void Awake()
		{
			if (obj == null) obj = new UsableItem();
		}
	}
}
