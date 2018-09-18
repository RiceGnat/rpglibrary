using UnityEditor;
using static Davfalcon.Revelator.Unity.Editor.EditorGUIHelper;
using static UnityEditor.EditorGUILayout;

namespace Davfalcon.Revelator.Unity.Editor
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
			buff.Duration = IntField("Default duration", buff.Duration);
			buff.SuccessChance = IntField("Default chance", buff.SuccessChance);

			Space();

			RenderStatModifiers(buff, ref ((BuffDefinition)target).statsExpanded);

			Space();

			RenderEffectsList("Upkeep effects", buff.UpkeepEffects, ref ((BuffDefinition)target).effectsExpanded);

			CheckChanged(target);
		}
	}
}
