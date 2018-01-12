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

			EditorGUIUtility.labelWidth = 75;

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Description");
			equipment.Description = EditorGUILayout.TextArea(equipment.Description);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.Space();

			equipment.Slot = (EquipmentSlot)EditorGUILayout.EnumPopup("Slot", equipment.Slot);

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
				equipment.Additions[stat] = EditorGUILayout.IntField(equipment.Additions[stat]);
				equipment.Multiplications[stat] = EditorGUILayout.IntField(equipment.Multiplications[stat]);
				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.Space();

			EditorGUILayout.LabelField("Granted buffs");
			for (int i = 0; i < equipment.GrantedBuffs.Count; i++)
			{
				BuffDefinition selected = null;
				if (equipment.GrantedBuffs[i] != null)
				{
					string guid = AssetDatabase.FindAssets(equipment.GrantedBuffs[i].Name).FirstOrDefault();
					if (guid != null)
					{
						selected = AssetDatabase.LoadAssetAtPath<BuffDefinition>(AssetDatabase.GUIDToAssetPath(guid));
					}
				}

				EditorGUILayout.BeginHorizontal();
				selected = (BuffDefinition)EditorGUILayout.ObjectField(selected, typeof(BuffDefinition), false);
				if (selected != null)
				{
					selected.OnAfterDeserialize();
					equipment.GrantedBuffs[i] = selected.buff;
				}

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

			if (GUI.changed)
				EditorUtility.SetDirty(target);
		}
	}
}