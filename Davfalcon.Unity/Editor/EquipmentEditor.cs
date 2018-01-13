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

			equipment.Slot = (EquipmentSlot)EnumPopup("Slot", equipment.Slot);

			Space();

			RenderStatModifiers(equipment, ref ((EquipmentDefinition)target).statsExpanded);

			Space();

			RenderBuffsList(equipment.GrantedBuffs, "Granted buffs", ref ((EquipmentDefinition)target).buffsExpanded);

			CheckChanged(target);
		}
	}
}