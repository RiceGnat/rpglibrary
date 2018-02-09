using UnityEngine;

namespace UnityEditor
{
	public static class EditorHelper
	{
		public static GameObject CreateGameObjectWithComponent<T>(string name, MenuCommand menuCommand) where T : Component
		{
			GameObject go = new GameObject(name);
			go.AddComponent<T>();
			GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
			Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
			Selection.activeObject = go;
			return go;
		}
	}
}
