using RPGLibrary;
using UnityEditor;
using UnityEngine;
using static UnityEditor.EditorGUILayout;

namespace Davfalcon.Unity.Editor
{
	public static class EditorGUIHelper
	{
		public static void SetLabelWidth()
		{
			EditorGUIUtility.labelWidth = 75;
		}

		public static void CheckChanged(Object target)
		{
			if (GUI.changed) EditorUtility.SetDirty(target);
		}

		public static void RenderStatModifiers(IStatsModifierEditable modifier, ref bool expanded)
		{
			BeginHorizontal();
			expanded = Foldout(expanded, "Stats", true, new GUIStyle(EditorStyles.foldout)
			{
				fixedWidth = EditorGUIUtility.labelWidth
			});
			if (expanded)
			{
				LabelField("", GUILayout.Width(15), GUILayout.ExpandWidth(false));
				LabelField("Additions", GUILayout.MinWidth(0));
				LabelField("Multipliers", GUILayout.MinWidth(0));
			}
			EndHorizontal();

			if (expanded)
			{
				foreach (string stat in UnitStats.GetAllStatNames())
				{
					BeginHorizontal();
					LabelField(stat, GUILayout.MaxWidth(EditorGUIUtility.labelWidth - 4));
					modifier.Additions[stat] = IntField(modifier.Additions[stat]);
					modifier.Multiplications[stat] = IntField(modifier.Multiplications[stat]);
					EndHorizontal();
				}
			}
		}
	}
}
