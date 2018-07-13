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

			bool wasWeapon = equipment.SlotType == EquipmentType.Weapon;
			equipment.SlotType = (EquipmentType)EnumPopup("Equipment slot", equipment.SlotType);
			bool isWeapon = equipment.SlotType == EquipmentType.Weapon;

			if (wasWeapon != isWeapon)
			{
				equipment = isWeapon ? new Weapon() : new Equipment(equipment.SlotType);
				((EquipmentDefinition)target).obj = equipment;
			}

			Space();

			if (isWeapon)
			{
				Weapon weapon = equipment as Weapon;

				weapon.BaseDamage = IntField("Base damage", weapon.BaseDamage);
				weapon.CritMultiplier = IntSlider("Crit multiplier", weapon.CritMultiplier, 1, 10);
				weapon.Type = (WeaponType)EnumPopup("Weapon type", weapon.Type);
				weapon.AttackElement = (Element)EnumPopup("Attack element", weapon.AttackElement);

				Space();

				RenderEffectsList("On-hit effects", weapon.OnHitEffects, ref ((EquipmentDefinition)target).effectsExpanded);

				Space();
			}

			RenderStatModifiers(equipment, ref ((EquipmentDefinition)target).statsExpanded);

			Space();

			RenderBuffsList("Granted buffs", equipment.GrantedBuffs, false, ref ((EquipmentDefinition)target).buffsExpanded);

			CheckChanged(target);
		}
	}
}