using System;
using UnityEditor;
using UnityEngine;
using static Davfalcon.Unity.Editor.EditorGUIHelper;
using static UnityEditor.EditorGUILayout;

namespace Davfalcon.Unity.Editor
{
	[CustomEditor(typeof(UnitTemplate))]
	public class UnitEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			Unit unit = ((UnitTemplate)target).obj;
			SetLabelWidth();

			Space();

			unit.Level = IntField("Level", unit.Level);
			unit.Class = TextField("Class", unit.Class);

			Space();

			ref bool attributesExpanded = ref ((UnitTemplate)target).attributesExpanded;
			attributesExpanded = Foldout(attributesExpanded, "Attributes", true, new GUIStyle(EditorStyles.foldout)
			{
				fixedWidth = EditorGUIUtility.labelWidth
			});

			if (attributesExpanded)
			{
				foreach (string stat in Enum.GetNames(typeof(Attributes)))
				{
					unit.BaseStats[stat] = IntField(stat, unit.BaseStats[stat]);
				}
			}

			Space();

			RenderEquipmentSlots("Equipment", unit.Properties.GetAs<IUnitItemProperties>(), ref ((UnitTemplate)target).equipmentExpanded);

			Space();

			ref bool statsExpanded = ref ((UnitTemplate)target).statsExpanded;
			statsExpanded = Foldout(statsExpanded, "Stats", true, new GUIStyle(EditorStyles.foldout)
			{
				fixedWidth = EditorGUIUtility.labelWidth
			});

			if (statsExpanded)
			{
				foreach (string stat in UnitStats.GetAllStatNames())
				{
					LabelField(stat, unit.Stats[stat].ToString());
				}
			}

			Space();

			RenderInventory("Inventory", unit.Properties.GetAs<IUnitItemProperties>().Inventory, ref ((UnitTemplate)target).inventoryExpanded);

			CheckChanged(target);
		}
	}
}