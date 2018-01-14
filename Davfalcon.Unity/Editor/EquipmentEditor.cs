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

			bool wasWeapon = equipment.Slot == EquipmentSlot.Weapon;
			equipment.Slot = (EquipmentSlot)EnumPopup("Equipment slot", equipment.Slot);
			bool isWeapon = equipment.Slot == EquipmentSlot.Weapon;

			if (wasWeapon != isWeapon)
			{
				equipment = isWeapon ? new Weapon() : new Equipment(equipment.Slot);
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

				RenderEffectsList(weapon.OnHitEffects, "On-hit effects", ref ((EquipmentDefinition)target).effectsExpanded);

				Space();
			}

			RenderStatModifiers(equipment, ref ((EquipmentDefinition)target).statsExpanded);

			Space();

			RenderBuffsList(equipment.GrantedBuffs, "Granted buffs", false, ref ((EquipmentDefinition)target).buffsExpanded);

			CheckChanged(target);
		}
	}
}