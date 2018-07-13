using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Saffron;
using UnityEditor;
using UnityEngine;
using static UnityEditor.EditorGUILayout;

namespace Davfalcon.Unity.Editor
{
	public static class EditorGUIHelper
	{
		public static T FindAssetWithName<T>(string name) where T : UnityEngine.Object
		{
			string guid = AssetDatabase.FindAssets(name).FirstOrDefault();
			if (guid != null)
			{
				return AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guid));
			}
			else return null;
		}

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

		public static Tobj RenderMappedObjectField<Tobj, Tdef>(Tobj obj, bool alwaysRefresh)
			where Tobj : class, INameable
			where Tdef : ObjectContainer
			=> RenderMappedObjectField<Tobj, Tdef>(null, obj, alwaysRefresh);

		public static Tobj RenderMappedObjectField<Tobj, Tdef>(string label, Tobj obj, bool alwaysRefresh)
			where Tobj : class, INameable
			where Tdef : ObjectContainer
		{
			Tdef selected = null;
			if (obj != null)
			{
				selected = FindAssetWithName<Tdef>(obj.Name);
			}

			selected = String.IsNullOrEmpty(label)
				? (Tdef)ObjectField(selected, typeof(Tdef), false)
				: (Tdef)ObjectField(label, selected, typeof(Tdef), false);
			if (selected != null)
			{
				if (obj == null || obj.Name != selected.name)
				{
					return selected.GetObjectAs<Tobj>();
				}
				else return alwaysRefresh ? selected.GetObjectAs<Tobj>() : obj;
			}
			else return null;
		}

		public static void RenderStatColumn(string label, IEditableStats stats, ref bool expanded)
			=> RenderStatColumns(label, stats, null, null, ref expanded);

		public static void RenderStatColumns(string col1Label, IEditableStats col1Stats, string col2Label, IEditableStats col2Stats, ref bool expanded)
		{
			BeginHorizontal();
			expanded = Foldout(expanded, "Stats", true, new GUIStyle(EditorStyles.foldout)
			{
				fixedWidth = EditorGUIUtility.labelWidth
			});
			if (expanded)
			{
				LabelField("", GUILayout.Width(40), GUILayout.ExpandWidth(false));
				LabelField(col1Label, GUILayout.MinWidth(0));
				if (col2Label != null) LabelField(col2Label, GUILayout.MinWidth(0));
			}
			EndHorizontal();

			if (expanded)
			{
				foreach (string stat in UnitStats.GetAllStatNames())
				{
					BeginHorizontal();

					LabelField(stat, GUILayout.MaxWidth(EditorGUIUtility.labelWidth - 4));
					col1Stats[stat] = IntField(col1Stats[stat]);
					if (col2Stats != null) col2Stats[stat] = IntField(col2Stats[stat]);

					EndHorizontal();
				}
			}
		}

		public static void RenderStatModifiers(IEditableStatsModifier modifier, ref bool expanded)
			=> RenderStatColumns("Additions", modifier.Additions, "Multiplications", modifier.Multiplications, ref expanded);

		public static void RenderEquipmentTypes(string label, IUnitItemProperties equipProps, ref bool expanded)
		{
			expanded = Foldout(expanded, label, true);
			if (expanded)
			{
				int i = 0;
				foreach (EquipmentType slot in equipProps.EquipmentSlots)
				{
					IEquipment selected = RenderMappedObjectField<IEquipment, EquipmentDefinition>(slot.ToString(), equipProps.GetEquipment(slot), true);

					if (selected != null)
					{
						if (selected.SlotType == slot)
							equipProps.EquipSlotIndex(selected, i);
					}
					else equipProps.UnequipSlotIndex(i);

					i++;
				}
			}
		}

		public static void RenderInventory(string label, IList<IItem> inventory, ref bool expanded)
		{
			expanded = Foldout(expanded, label, true);
			if (expanded)
			{
				for (int i = 0; i < inventory.Count; i++)
				{
					BeginHorizontal();
					inventory[i] = RenderMappedObjectField<IItem, ObjectContainer>(null, inventory[i], false); ;

					if (GUILayout.Button("Remove", EditorStyles.miniButton))
					{
						inventory.RemoveAt(i);
						i--;
						continue;
					}
					EndHorizontal();
				}
				if (GUILayout.Button("Add"))
				{
					inventory.Add(null);
				}
			}
		}

		public static void RenderBuffsList(string label, IList<IBuff> buffs, bool showParams, ref bool expanded)
		{
			expanded = Foldout(expanded, label, true);
			if (expanded)
			{
				for (int i = 0; i < buffs.Count; i++)
				{
					BeginHorizontal();
					buffs[i] = RenderMappedObjectField<IBuff, BuffDefinition>(null, buffs[i], false); ;

					if (GUILayout.Button("Remove", EditorStyles.miniButton))
					{
						buffs.RemoveAt(i);
						i--;
						continue;
					}
					EndHorizontal();

					if (showParams && buffs[i] != null)
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

		public static void RenderEffectsList(string label, IEffectList effects, ref bool expanded)
		{
			expanded = Foldout(expanded, label, true);
			if (expanded)
			{
				for (int i = 0; i < effects.Count; i++)
				{
					int selected = 0;
					List<string> names = SystemDataService.current.effects.Names.ToList();
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
