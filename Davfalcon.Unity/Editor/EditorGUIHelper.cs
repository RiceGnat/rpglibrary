using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RPGLibrary;
using UnityEditor;
using UnityEngine;
using static Davfalcon.Engine.SystemData;
using static UnityEditor.EditorGUILayout;

namespace Davfalcon.Unity.Editor
{
	public static class EditorGUIHelper
	{
		public static void SetLabelWidth()
		{
			EditorGUIUtility.labelWidth = 100;
		}

		public static void CheckChanged(UnityEngine.Object target)
		{
			if (GUI.changed) EditorUtility.SetDirty(target);
		}

		public static void RenderDescriptionField(IEditableDescription item)
		{
			BeginHorizontal();
			PrefixLabel("Description");
			item.Description = TextArea(item.Description);
			EndHorizontal();
		}

		public static void RenderStatModifiers(IEditableStatsModifier modifier, ref bool expanded)
		{
			BeginHorizontal();
			expanded = Foldout(expanded, "Stats", true, new GUIStyle(EditorStyles.foldout)
			{
				fixedWidth = EditorGUIUtility.labelWidth
			});
			if (expanded)
			{
				LabelField("", GUILayout.Width(40), GUILayout.ExpandWidth(false));
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

		public static void RenderBuffsList(IList<IBuff> buffs, string label, bool showParams, ref bool expanded)
		{
			expanded = Foldout(expanded, label, true);
			if (expanded)
			{
				for (int i = 0; i < buffs.Count; i++)
				{
					BuffDefinition selected = null;
					if (buffs[i] != null)
					{
						string guid = AssetDatabase.FindAssets(buffs[i].Name).FirstOrDefault();
						if (guid != null)
						{
							selected = AssetDatabase.LoadAssetAtPath<BuffDefinition>(AssetDatabase.GUIDToAssetPath(guid));
						}
					}

					BeginHorizontal();
					selected = (BuffDefinition)ObjectField(selected, typeof(BuffDefinition), false);
					if (selected != null)
					{
						selected.OnAfterDeserialize();

						if (buffs[i] == null || buffs[i].Name != selected.name)
							buffs[i] = selected.obj;
					}
					else
					{
						buffs[i] = null;
					}

					if (GUILayout.Button("Remove", EditorStyles.miniButton))
					{
						buffs.RemoveAt(i);
						i--;
					}
					EndHorizontal();

					if (showParams && selected != null)
					{
						float labelWidth = EditorGUIUtility.labelWidth;
						EditorGUIUtility.labelWidth = 60;
						BeginHorizontal();
						buffs[i].Duration = IntField("Duration", buffs[i].Duration);
						GUILayout.Space(10);
						buffs[i].SuccessChance = IntField("Chance", buffs[i].SuccessChance);
						EndHorizontal();
						EditorGUIUtility.labelWidth = labelWidth;
					}
				}
				if (GUILayout.Button("Add"))
				{
					buffs.Add(null);
				}
			}
		}

		public static void RenderEffectsList(IEffectList effects, string label, ref bool expanded)
		{
			expanded = Foldout(expanded, label, true);
			if (expanded)
			{
				for (int i = 0; i < effects.Count; i++)
				{
					int selected = 0;
					List<string> names = Current.Effects.Names.ToList();
					if (effects[i] != null)
					{
						selected = names.IndexOf(effects[i].Name);
					}

					BeginHorizontal();
					selected = Popup(selected, names.ToArray());

					ArrayList args = new ArrayList(effects[i].Args);

					if (GUILayout.Button("Add param", EditorStyles.miniButton))
					{
						args.Add(0);
					}

					if (GUILayout.Button("Remove effect", EditorStyles.miniButton))
					{
						effects.RemoveAt(i);
						i--;
						continue;
					}
					EndHorizontal();

					for (int j = 0; j < args.Count; j++)
					{
						BeginHorizontal();
						LabelField(String.Format("[{0}]", j), GUILayout.MaxWidth(25));

						int selectedType = args[j] is string ? 1 : 0;
						int newType = Popup(selectedType, new string[] { "int", "string" }, GUILayout.MaxWidth(50));

						if (newType != selectedType)
							args[j] = newType == 1 ? (object)"" : 0;

						args[j] = newType == 0 ? (object)IntField((int)args[j]) : TextField((string)args[j]);

						if (GUILayout.Button("Remove param", EditorStyles.miniButton))
						{
							args.RemoveAt(j);
							j--;
						}

						EndHorizontal();
					}

					if (selected >= 0)
						effects[i] = new EffectArgs(names[selected], args.ToArray());
				}
				if (GUILayout.Button("Add"))
				{
					effects.Add(null);
				}
			}
		}
	}
}
