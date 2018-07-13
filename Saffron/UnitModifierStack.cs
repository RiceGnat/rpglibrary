using System;
using System.Collections.Generic;

namespace Saffron
{
	/// <summary>
	/// Manages a group of modifiers. Can function as a single modifier.
	/// </summary>
	[Serializable]
	public class UnitModifierStack : UnitModifier, IUnitModifierStack
	{
		private List<IUnitModifier> stack = new List<IUnitModifier>();

		protected override IUnit InterfaceUnit => stack.Count > 0 ? stack[stack.Count - 1] : Target;

		public int Count => stack.Count;

		public override void Bind(IUnit target)
		{
			// Set target
			base.Bind(target);

			BindStack();
		}

		private void BindStack()
		{
			if (stack.Count > 0)
			{
				// Bind first modifier in the stack to the target
				stack[0].Bind(Target);

				// Rebind rest of stack
				for (int i = 1; i < stack.Count; i++)
				{
					stack[i].Bind(stack[i - 1]);
				}
			}
		}

		public void Add(IUnitModifier item)
		{
			item.Bind(InterfaceUnit);
			stack.Add(item);
		}

		public bool Remove(IUnitModifier item)
		{
			// Search for referenced item
			int index = stack.FindIndex(X => X.Equals(item));

			// Not found
			if (index == -1) return false;

			RemoveAt(index);
			return true;
		}

		public void RemoveAt(int index)
		{
			stack.RemoveAt(index);
			BindStack();
		}

		public void Clear()	=> stack.Clear();

		#region IEnumerable implementation
		private IEnumerator<IUnitModifier> GetEnumerator()
			=> stack.GetEnumerator();

		IEnumerator<IUnitModifier> IEnumerable<IUnitModifier>.GetEnumerator()
			=> GetEnumerator();

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
			=> GetEnumerator();
		#endregion
	}
}
