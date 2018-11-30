using System;
using System.Collections;
using System.Collections.Generic;

namespace Davfalcon
{
	/// <summary>
	/// Manages a collection of modifiers. Can function as a single modifier.
	/// </summary>
	[Serializable]
	public abstract class ModifierCollection<T> : Modifier<T>, IModifierCollection<T>
	{
		private List<IModifier<T>> stack = new List<IModifier<T>>();

		protected virtual T Interface => stack.Count > 0 ? (T)stack[stack.Count - 1] : Target;

		public override void Bind(T target)
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

		public int Count => stack.Count;

		public void Add(IModifier<T> item)
		{
			if (!(item is T)) throw new NotSupportedException(nameof(item));
			item.Bind(Interface);
			stack.Add(item);
		}

		public bool Remove(IModifier<T> item)
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

		public void Clear() => stack.Clear();

		private IEnumerator<IModifier<T>> GetEnumerator() => stack.GetEnumerator();
		IEnumerator<IModifier<T>> IEnumerable<IModifier<T>>.GetEnumerator() => GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
	}
}
