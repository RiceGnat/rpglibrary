using UnityEngine;

namespace Davfalcon.Revelator.Unity
{
	[CreateAssetMenu(menuName = "Spell", fileName = "Spell")]
	public class SpellDefinition : NameableSerializationContainer<Spell>
	{
		public bool buffsExpanded = true;
		public bool effectsExpanded = true;
	}
}
