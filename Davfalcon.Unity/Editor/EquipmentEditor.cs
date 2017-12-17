using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

namespace Davfalcon.Unity.Editor
{
	public class EquipmentEditor : EditorWindow
	{

		public EquipmentDefinition equipment;
		private int viewIndex = 1;

		[MenuItem("Window/Equipment Editor %#e")]
		static void Init()
		{
			EditorWindow.GetWindow(typeof(EquipmentEditor));
		}

		void OnEnable()
		{
			if (EditorPrefs.HasKey("ObjectPath"))
			{
				string objectPath = EditorPrefs.GetString("ObjectPath");
				equipment = AssetDatabase.LoadAssetAtPath(objectPath, typeof(EquipmentDefinition)) as EquipmentDefinition;
			}

		}

		void OnGUI()
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label("Equipment Editor", EditorStyles.boldLabel);
			GUILayout.EndHorizontal();

			if (inventoryItemList == null)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Space(10);
				if (GUILayout.Button("Create New Item List", GUILayout.ExpandWidth(false)))
				{
					CreateNewItemList();
				}
				if (GUILayout.Button("Open Existing Item List", GUILayout.ExpandWidth(false)))
				{
					OpenItemList();
				}
				GUILayout.EndHorizontal();
			}

			GUILayout.Space(20);

			if (inventoryItemList != null)
			{
				GUILayout.BeginHorizontal();

				GUILayout.Space(10);

				if (GUILayout.Button("Prev", GUILayout.ExpandWidth(false)))
				{
					if (viewIndex > 1)
						viewIndex--;
				}
				GUILayout.Space(5);
				if (GUILayout.Button("Next", GUILayout.ExpandWidth(false)))
				{
					if (viewIndex < inventoryItemList.itemList.Count)
					{
						viewIndex++;
					}
				}

				GUILayout.Space(60);

				if (GUILayout.Button("Add Item", GUILayout.ExpandWidth(false)))
				{
					AddItem();
				}
				if (GUILayout.Button("Delete Item", GUILayout.ExpandWidth(false)))
				{
					DeleteItem(viewIndex - 1);
				}

				GUILayout.EndHorizontal();
				if (inventoryItemList.itemList == null)
					Debug.Log("wtf");
				if (inventoryItemList.itemList.Count > 0)
				{
					GUILayout.BeginHorizontal();
					viewIndex = Mathf.Clamp(EditorGUILayout.IntField("Current Item", viewIndex, GUILayout.ExpandWidth(false)), 1, inventoryItemList.itemList.Count);
					//Mathf.Clamp (viewIndex, 1, inventoryItemList.itemList.Count);
					EditorGUILayout.LabelField("of   " + inventoryItemList.itemList.Count.ToString() + "  items", "", GUILayout.ExpandWidth(false));
					GUILayout.EndHorizontal();

					inventoryItemList.itemList[viewIndex - 1].itemName = EditorGUILayout.TextField("Item Name", inventoryItemList.itemList[viewIndex - 1].itemName as string);
					inventoryItemList.itemList[viewIndex - 1].itemIcon = EditorGUILayout.ObjectField("Item Icon", inventoryItemList.itemList[viewIndex - 1].itemIcon, typeof(Texture2D), false) as Texture2D;
					inventoryItemList.itemList[viewIndex - 1].itemObject = EditorGUILayout.ObjectField("Item Object", inventoryItemList.itemList[viewIndex - 1].itemObject, typeof(Rigidbody), false) as Rigidbody;

					GUILayout.Space(10);

					GUILayout.BeginHorizontal();
					inventoryItemList.itemList[viewIndex - 1].isUnique = (bool)EditorGUILayout.Toggle("Unique", inventoryItemList.itemList[viewIndex - 1].isUnique, GUILayout.ExpandWidth(false));
					inventoryItemList.itemList[viewIndex - 1].isIndestructible = (bool)EditorGUILayout.Toggle("Indestructable", inventoryItemList.itemList[viewIndex - 1].isIndestructible, GUILayout.ExpandWidth(false));
					inventoryItemList.itemList[viewIndex - 1].isQuestItem = (bool)EditorGUILayout.Toggle("QuestItem", inventoryItemList.itemList[viewIndex - 1].isQuestItem, GUILayout.ExpandWidth(false));
					GUILayout.EndHorizontal();

					GUILayout.Space(10);

					GUILayout.BeginHorizontal();
					inventoryItemList.itemList[viewIndex - 1].isStackable = (bool)EditorGUILayout.Toggle("Stackable ", inventoryItemList.itemList[viewIndex - 1].isStackable, GUILayout.ExpandWidth(false));
					inventoryItemList.itemList[viewIndex - 1].destroyOnUse = (bool)EditorGUILayout.Toggle("Destroy On Use", inventoryItemList.itemList[viewIndex - 1].destroyOnUse, GUILayout.ExpandWidth(false));
					inventoryItemList.itemList[viewIndex - 1].encumbranceValue = EditorGUILayout.FloatField("Encumberance", inventoryItemList.itemList[viewIndex - 1].encumbranceValue, GUILayout.ExpandWidth(false));
					GUILayout.EndHorizontal();

					GUILayout.Space(10);

				}
				else
				{
					GUILayout.Label("This Inventory List is Empty.");
				}
			}
			if (GUI.changed)
			{
				EditorUtility.SetDirty(equipment);
			}
		}
	}
}