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

		public void Show()
		{
			Displayed = this;

			Draw();
		}
	}
}
