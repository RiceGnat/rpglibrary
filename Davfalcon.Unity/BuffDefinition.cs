using UnityEngine;

namespace Davfalcon.Unity
{
	[CreateAssetMenu(menuName = "Buff")]
	public class BuffDefinition : NameableSerializationContainer<Buff>
	{
		public bool statsExpanded = true;
		public bool effectsExpanded = true;

		private void Awake()
		{
			obj = new Buff();
		}
	}
}
