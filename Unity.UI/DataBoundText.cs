namespace UnityEngine.UI
{
	[RequireComponent(typeof(Text))]
	public class DataBoundText : DataBoundElement
	{
		private Text _text;
		public Text text => this.SetFieldAndReturnComponent(ref _text);

		public override void Draw()
		{
			text.text = GetDataAs<object>()?.ToString();
			base.Draw();
		}
	}
}
