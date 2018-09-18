using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityEngine.UI
{
	/// <summary>
	/// Top level UI element. Only one should be displayed at a time.
	/// </summary>
	public class Screen : Element
	{
		public static Screen Displayed { get; private set; }

		public bool IsVisible => Displayed.Equals(this);

		public Menu initialFocus;

		private void Hide()
		{
			gameObject.SetActive(false);
		}

		public void Show()
		{
			if (!Equals(Displayed))
			{
				if (Displayed != null) {
					Displayed.Hide();
				}

				Displayed = this;
				gameObject.SetActive(true);
				Draw();

				if (initialFocus != null)
					initialFocus.Focus();
			}
		}
	}
}
