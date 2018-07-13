using UnityEngine;

namespace Davfalcon.Unity
{
	[CreateAssetMenu(menuName = "Spell", fileName = "Spell")]
	public class SpellDefinition : NameableSerializationContainer<Spell>
	{
		public bool buffsExpanded = true;
		public bool effectsExpanded = true;
	}
}
