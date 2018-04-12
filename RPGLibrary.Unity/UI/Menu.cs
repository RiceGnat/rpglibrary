using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnityEngine.UI
{
	/// <summary>
	/// Represents a group of related UI elements that the user is interacting with. Only one should be focused to accept inputs at a time.
	/// </summary>
	public class Menu : Element
	{
		public static Menu Focused { get; private set; }

		public bool IsFocused => Focused.Equals(this);

		public void Focus()
		{
			Focused = this;
		}
	}
}
