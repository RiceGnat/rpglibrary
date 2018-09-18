using UnityEngine;

namespace UnityEditor
{
	/// <summary>
	/// Unity editor helper functions
	/// </summary>
	public static class EditorHelper
	{
		/// <summary>
		/// Creates a GameObject with a given component via menu command.
		/// </summary>
		/// <typeparam name="T">The type of the component attached to the new GameObject.</typeparam>
		/// <param name="name">The name of the new GameObject.</param>
		/// <param name="menuCommand">The menu command used.</param>
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
