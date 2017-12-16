namespace UnityEngine.UI
{
	[RequireComponent(typeof(RectTransform))]
	[DisallowMultipleComponent]
	public class Element : MonoBehaviour
	{
		private RectTransform _rectTransform;
		protected RectTransform rectTransform => this.SetFieldAndReturnComponent(ref _rectTransform);

		public float height => rectTransform.rect.height;
		public float width => rectTransform.rect.width;

		public void Shift(float x, float y) => rectTransform.anchoredPosition += new Vector2(x, -y);
		public void ShiftDown(float y) => Shift(0, y);
		public void ShiftRight(float x) => Shift(x, 0);

		public virtual void Draw()
		{
			foreach (Element e in GetComponentsInChildren<Element>())
			{
				if (!e.Equals(this)) e.Draw();
			}
		}
	}
}
