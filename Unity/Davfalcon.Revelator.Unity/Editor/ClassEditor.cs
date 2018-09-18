using UnityEditor;
using static Davfalcon.Revelator.Unity.Editor.EditorGUIHelper;
using static UnityEditor.EditorGUILayout;

namespace Davfalcon.Revelator.Unity.Editor
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
