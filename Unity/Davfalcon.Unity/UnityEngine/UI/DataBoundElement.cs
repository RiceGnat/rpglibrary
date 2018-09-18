namespace UnityEngine.UI
{
	/// <summary>
	/// Binds data to a UI element.
	/// </summary>
	public class DataBoundElement : Element
	{
		private object data;

		public void Bind(object data) => this.data = data;
		public T GetDataAs<T>() => (T)data;
	}
}
