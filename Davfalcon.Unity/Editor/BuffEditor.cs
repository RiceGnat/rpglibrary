using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Davfalcon.Unity.Editor
{
	[CustomEditor(typeof(BuffDefinition))]
	public class BuffEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			Buff buff = ((BuffDefinition)target).buff;

			float labelWidth = EditorGUIUtility.labelWidth;

			EditorGUIUtility.labelWidth = 75;
			buff.Name = EditorGUILayout.TextField("Name", buff.Name);

			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.MaxWidth(EditorGUIUtility.labelWidth - 4));
			EditorGUILayout.LabelField("Additions", GUILayout.MinWidth(0));
			EditorGUILayout.LabelField("Multipliers", GUILayout.MinWidth(0));
			EditorGUILayout.EndHorizontal();

			List<string> stats = new List<string>();
			stats.AddRange(Enum.GetNames(typeof(Attributes)));
			stats.AddRange(Enum.GetNames(typeof(CombatStats)));
			foreach (string stat in stats)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(stat, GUILayout.MaxWidth(EditorGUIUtility.labelWidth - 4));
				buff.Additions[stat] = EditorGUILayout.IntField(buff.Additions[stat]);
				buff.Multiplications[stat] = EditorGUILayout.IntField(buff.Multiplications[stat]);
				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.Space();


		}
	}
}
