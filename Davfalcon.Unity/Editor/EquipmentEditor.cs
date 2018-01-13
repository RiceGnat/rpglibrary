using System.Linq;
using UnityEditor;
using UnityEngine;
using static UnityEditor.EditorGUILayout;

namespace Davfalcon.Unity.Editor
{
	[CustomEditor(typeof(EquipmentDefinition))]
	public class EquipmentEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			Equipment equipment = ((EquipmentDefinition)target).obj;
			EditorGUIHelper.SetLabelWidth();

			BeginHorizontal();
			PrefixLabel("Description");
			equipment.Description = TextArea(equipment.Description);
			EndHorizontal();

			Space();

			equipment.Slot = (EquipmentSlot)EnumPopup("Slot", equipment.Slot);

			Space();

			EditorGUIHelper.RenderStatModifiers(equipment, ref ((EquipmentDefinition)target).statsExpanded);

			Space();

			ref bool expanded = ref ((EquipmentDefinition)target).buffsExpanded;
			expanded = Foldout(expanded, "Granted buffs", true);
			if (expanded)
			{
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

					BeginHorizontal();
					selected = (BuffDefinition)ObjectField(selected, typeof(BuffDefinition), false);
					if (selected != null)
					{
						selected.OnAfterDeserialize();
						equipment.GrantedBuffs[i] = selected.obj;
					}

					if (GUILayout.Button("Remove", EditorStyles.miniButton))
					{
						equipment.GrantedBuffs.RemoveAt(i);
						i--;
					}
					EndHorizontal();
				}
				if (GUILayout.Button("Add"))
				{
					equipment.GrantedBuffs.Add(null);
				}
			}

			EditorGUIHelper.CheckChanged(target);
		}
	}
}