using UnityEditor;

namespace UnityEngine.UI
{
	[RequireComponent(typeof(Text))]
	public class DataBoundText : DataBoundElement
	{
		private Text _text;
		public Text text => this.SetFieldAndReturnComponent(ref _text);

		public override void Draw()
		{
			text.text = GetDataAs<object>().ToString();
			base.Draw();
		}

		[MenuItem("GameObject/UI/Data bound text", false, 10)]
		private static void Create(MenuCommand menuCommand)
		{
			EditorHelper.CreateGameObjectWithComponent<DataBoundText>("Data bound text", menuCommand);
		}
	}
}
