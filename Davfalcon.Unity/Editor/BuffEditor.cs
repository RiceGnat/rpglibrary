using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static Davfalcon.Engine.SystemData;

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

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Description");
			buff.Description = EditorGUILayout.TextArea(buff.Description);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.Space();

			buff.IsDebuff = EditorGUILayout.Toggle("Debuff", buff.IsDebuff);
			buff.Duration = EditorGUILayout.IntField("Duration", buff.Duration);

			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("", GUILayout.MaxWidth(EditorGUIUtility.labelWidth - 4));
			EditorGUILayout.LabelField("Additions", GUILayout.MinWidth(0));
			EditorGUILayout.LabelField("Multipliers", GUILayout.MinWidth(0));
			EditorGUILayout.EndHorizontal();

			foreach (string stat in UnitStats.GetAllStatNames())
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.LabelField(stat, GUILayout.MaxWidth(EditorGUIUtility.labelWidth - 4));
				buff.Additions[stat] = EditorGUILayout.IntField(buff.Additions[stat]);
				buff.Multiplications[stat] = EditorGUILayout.IntField(buff.Multiplications[stat]);
				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Upkeep effects");

			for (int i = 0; i < buff.UpkeepEffects.Count; i++)
			{
				int selected = 0;
				List<string> names = Current.Effects.Names.ToList();
				if (buff.UpkeepEffects[i] != null)
				{
					selected = names.IndexOf(buff.UpkeepEffects[i].Name);
				}

				EditorGUILayout.BeginHorizontal();
				selected = EditorGUILayout.Popup(selected, names.ToArray());

				ArrayList args = new ArrayList(buff.UpkeepEffects[i].Args);

				if (GUILayout.Button("Add param", EditorStyles.miniButton))
				{
					args.Add(0);
				}

				if (GUILayout.Button("Remove effect", EditorStyles.miniButton))
				{
					buff.UpkeepEffects.RemoveAt(i);
					i--;
					continue;
				}
				EditorGUILayout.EndHorizontal();
				
				for (int j = 0; j < args.Count; j++)
				{
					EditorGUILayout.BeginHorizontal(GUILayout.Height(EditorGUIUtility.singleLineHeight));
					EditorGUILayout.LabelField(String.Format("[{0}]", j), GUILayout.MaxWidth(25));

					int selectedType = args[j] is string ? 1 : 0;
					int newType = EditorGUILayout.Popup(selectedType, new string[] { "int", "string" }, GUILayout.MaxWidth(50));

					if (newType != selectedType)
					{
						args[j] = newType == 1 ? (object)"" : 0;
					}

					if (newType == 0)
					{
						args[j] = EditorGUILayout.IntField((int)args[j]);
					}
					else
					{
						args[j] = EditorGUILayout.TextField((string)args[j]);
					}

					if (GUILayout.Button("Remove param", EditorStyles.miniButton))
					{
						args.RemoveAt(j);
						j--;
					}

					EditorGUILayout.EndHorizontal();
				}

				if (selected >= 0)
					buff.UpkeepEffects[i] = new EffectArgs(names[selected], args.ToArray());

				EditorGUILayout.Space();
			}
			if (GUILayout.Button("Add"))
			{
				buff.UpkeepEffects.Add(null);
			}

			if (GUI.changed)
				EditorUtility.SetDirty(target);
		}
	}
}
