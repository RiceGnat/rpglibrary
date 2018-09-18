namespace UnityEngine.UI
{
	/// <summary>
	/// Represents a group of related UI elements that the user is interacting with. Only one should be focused to accept inputs at a time.
	/// </summary>
	public class Menu : Element
	{
		public delegate void Callback(object o, Menu sender);

		public static Menu Focused { get; private set; }

		private Callback returnCallback;

		public bool IsFocused => Focused.Equals(this);

		public void Focus()
		{
			Focused = this;
		}

		public void Focus(Callback callback)
		{
			returnCallback = callback;
			Focus();
		}

		public void Show()
		{
			gameObject.SetActive(true);
			Draw();
		}

		protected virtual void Return(object o)
		{
			returnCallback?.Invoke(o, this);
		}

		protected Menu OpenChild(Menu prefab, Callback callback)
		{
			Menu child = Instantiate(prefab);
			child.transform.parent = transform;
			child.Show();
			child.Focus(callback);
			return child;
		}

		private void OnDestroy()
		{
			if (IsFocused)
				Focused = null;
		}
	}
}
