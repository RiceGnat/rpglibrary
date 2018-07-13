using UnityEngine;

namespace Davfalcon.Unity
{
	[CreateAssetMenu(menuName = "Buff", fileName = "Buff")]
	public class BuffDefinition : NameableSerializationContainer<Buff>
	{
		public bool statsExpanded = true;
		public bool effectsExpanded = true;
	}
}
