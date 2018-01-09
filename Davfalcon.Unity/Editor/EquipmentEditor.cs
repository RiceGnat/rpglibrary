using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Davfalcon.Unity.Editor
{
	[CustomEditor(typeof(EquipmentDefinition))]
	public class EquipmentEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			Equipment equipment = ((EquipmentDefinition)target).equipment;

			float labelWidth = EditorGUIUtility.labelWidth;

			EditorGUIUtility.labelWidth = 75;
			equipment.Name = EditorGUILayout.TextField("Name", equipment.Name);
			equipment.Slot = (EquipmentSlot)EditorGUILayout.EnumPopup("Slot", equipment.Slot);

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Description");
			equipment.Description = EditorGUILayout.TextArea(equipment.Description);
			EditorGUILayout.EndHorizontal();

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
				equipment.Additions[stat] = EditorGUILayout.IntField(equipment.Additions[stat]);
				equipment.Multiplications[stat] = EditorGUILayout.IntField(equipment.Multiplications[stat]);
				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.Space();

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField("Granted buffs");
			EditorGUILayout.EndHorizontal();
			for (int i = 0; i < equipment.GrantedBuffs.Count; i++)
			{
				BuffDefinition[] defs = Resources.FindObjectsOfTypeAll<BuffDefinition>();
				//List<string> names = defs.Select(b => b.name).ToList();
				BuffDefinition selected = defs.Where(b => b.buff.Name == equipment.GrantedBuffs[i]?.Name).FirstOrDefault();

				EditorGUILayout.BeginHorizontal();
				//equipment.GrantedBuffs[i] = defs[EditorGUILayout.Popup(selected, names.ToArray())].buff;
				equipment.GrantedBuffs[i] = ((BuffDefinition)EditorGUILayout.ObjectField(selected, typeof(BuffDefinition), false))?.buff;

				if (GUILayout.Button("Remove", EditorStyles.miniButton))
				{
					equipment.GrantedBuffs.RemoveAt(i);
					i--;
				}
				EditorGUILayout.EndHorizontal();
			}
			if (GUILayout.Button("Add"))
			{
				equipment.GrantedBuffs.Add(null);
			}
		}
	}
}