using System;
using Davfalcon.Engine;
using UnityEditor;
using UnityEngine;

namespace Davfalcon.Unity
{
	public class EquipmentDefinition : ScriptableObject
	{
		public Equipment equipment = new Equipment(EquipmentSlot.Armor);

		private void Awake()
		{
			equipment.Name = "New Equipment";
		}

		[MenuItem("Assets/Create/Equipment")]
		public static void CreateMyAsset()
		{
			EquipmentDefinition asset = ScriptableObject.CreateInstance<EquipmentDefinition>();

			AssetDatabase.CreateAsset(asset, "Assets/New Equipment.asset");
			AssetDatabase.SaveAssets();

			EditorUtility.FocusProjectWindow();

			Selection.activeObject = asset;
		}
	}
}
