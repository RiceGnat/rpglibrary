using UnityEditor;
using static Davfalcon.Unity.Editor.EditorGUIHelper;
using static UnityEditor.EditorGUILayout;

namespace Davfalcon.Unity.Editor
{
	[CustomEditor(typeof(BuffDefinition))]
	public class BuffEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			Buff buff = ((BuffDefinition)target).obj;
			SetLabelWidth();

			RenderDescriptionField(buff);

			Space();

			buff.IsDebuff = Toggle("Debuff", buff.IsDebuff);
			buff.Duration = IntField("Duration", buff.Duration);

			Space();

			RenderStatModifiers(buff, ref ((BuffDefinition)target).statsExpanded);

			Space();

			RenderEffectsList(buff.UpkeepEffects, "Upkeep effects", ref ((BuffDefinition)target).effectsExpanded);

			CheckChanged(target);
		}
	}
}
