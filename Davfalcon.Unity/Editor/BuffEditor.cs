using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using static Davfalcon.Engine.SystemData;
using static UnityEditor.EditorGUILayout;

namespace Davfalcon.Unity.Editor
{
	[CustomEditor(typeof(BuffDefinition))]
	public class BuffEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			Buff buff = ((BuffDefinition)target).obj;
			EditorGUIHelper.SetLabelWidth();

			BeginHorizontal();
			PrefixLabel("Description");
			buff.Description = TextArea(buff.Description);
			EndHorizontal();

			Space();

			buff.IsDebuff = Toggle("Debuff", buff.IsDebuff);
			buff.Duration = IntField("Duration", buff.Duration);

			Space();

			EditorGUIHelper.RenderStatModifiers(buff, ref ((BuffDefinition)target).statsExpanded);

			Space();

			ref bool expanded = ref ((BuffDefinition)target).effectsExpanded;
			expanded = Foldout(expanded, "Upkeep effects", true);
			if (expanded)
			{
				for (int i = 0; i < buff.UpkeepEffects.Count; i++)
				{
					int selected = 0;
					List<string> names = Current.Effects.Names.ToList();
					if (buff.UpkeepEffects[i] != null)
					{
						selected = names.IndexOf(buff.UpkeepEffects[i].Name);
					}

					BeginHorizontal();
					selected = Popup(selected, names.ToArray());

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
					EndHorizontal();

					for (int j = 0; j < args.Count; j++)
					{
						BeginHorizontal(GUILayout.Height(EditorGUIUtility.singleLineHeight));
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
						buff.UpkeepEffects[i] = new EffectArgs(names[selected], args.ToArray());

					Space();
				}
				if (GUILayout.Button("Add"))
				{
					buff.UpkeepEffects.Add(null);
				}
			}

			EditorGUIHelper.CheckChanged(target);
		}
	}
}
