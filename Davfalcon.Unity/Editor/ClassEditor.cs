using UnityEditor;
using static Davfalcon.Unity.Editor.EditorGUIHelper;
using static UnityEditor.EditorGUILayout;

namespace Davfalcon.Unity.Editor
{
	[CustomEditor(typeof(ClassDefinition))]
	public class ClassEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			ClassProperties classProps = ((ClassDefinition)target).obj;
			SetLabelWidth();

			RenderDescriptionField(classProps);

			Space();

			RenderStatColumns("Base", classProps.BaseStats, "Growths", classProps.StatGrowths, ref ((ClassDefinition)target).statsExpanded);

			CheckChanged(target);
		}
	}
}
