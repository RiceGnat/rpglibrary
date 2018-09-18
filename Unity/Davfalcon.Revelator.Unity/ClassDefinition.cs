using UnityEngine;

namespace Davfalcon.Revelator.Unity
{
	[CreateAssetMenu(menuName = "Class data", fileName = "Class")]
	public class ClassDefinition : NameableSerializationContainer<ClassProperties>
	{
		public bool statsExpanded = true;
	}
}
