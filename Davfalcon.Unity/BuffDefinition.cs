using UnityEditor;
using UnityEngine;

namespace Davfalcon.Unity
{
	public class BuffDefinition : ScriptableObject
	{
		public Buff buff = new Buff();

		private void Awake()
		{
			buff.Name = "New Buff";
		}

		[MenuItem("Assets/Create/Buff")]
		public static void CreateMyAsset()
		{
			BuffDefinition asset = ScriptableObject.CreateInstance<BuffDefinition>();

			AssetDatabase.CreateAsset(asset, "Assets/New Buff.asset");
			AssetDatabase.SaveAssets();

			EditorUtility.FocusProjectWindow();

			Selection.activeObject = asset;
		}
	}
}
