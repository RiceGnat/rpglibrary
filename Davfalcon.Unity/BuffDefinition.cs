using UnityEngine;

namespace Davfalcon.Unity
{
	[CreateAssetMenu(menuName = "Buff")]
	public class BuffDefinition : SerializationContainer<Buff>
	{
		public bool statsExpanded = true;
		public bool effectsExpanded = true;

		private void Awake()
		{
			obj = new Buff();
		}

		public override void SerializationPrep()
		{
			obj.Name = name;
		}
	}
}
