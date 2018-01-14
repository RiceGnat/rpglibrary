using UnityEditor;
using static Davfalcon.Unity.Editor.EditorGUIHelper;
using static UnityEditor.EditorGUILayout;

namespace Davfalcon.Unity.Editor
{
	[CustomEditor(typeof(EquipmentDefinition))]
	public class EquipmentEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			Equipment equipment = ((EquipmentDefinition)target).obj;
			SetLabelWidth();

			RenderDescriptionField(equipment);

			Space();

			// TODO: check if slot changed between weapon and non-weapon
			equipment.Slot = (EquipmentSlot)EnumPopup("Slot", equipment.Slot);
			// TODO: re-create object if changed

			// TODO: weapon properties here

			Space();

			RenderStatModifiers(equipment, ref ((EquipmentDefinition)target).statsExpanded);

			Space();

			RenderBuffsList(equipment.GrantedBuffs, "Granted buffs", false, ref ((EquipmentDefinition)target).buffsExpanded);

			CheckChanged(target);
		}
	}
}