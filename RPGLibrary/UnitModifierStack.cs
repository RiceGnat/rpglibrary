using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RPGLibrary
{
	/// <summary>
	/// Manages a group of modifiers. Can function as a single modifier.
	/// </summary>
	[Serializable]
	public class UnitModifierStack : UnitModifier, IUnitModifierStack
	{
		private List<IUnitModifier> stack = new List<IUnitModifier>();

		protected override IUnit InterfaceUnit
		{
			get
			{
				return stack.Count > 0 ? stack[stack.Count - 1] : Target;
			}
		}

		public int Count { get { return stack.Count; } }

		public override void Bind(IUnit target)
		{
			base.Bind(target);
			if (stack.Count > 0) stack[0].Bind(Target);
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

			// Item is first and there is an item above it
			if (index == 0 && stack.Count > 1)
			{
				stack[index + 1].Bind(Target);
			}
			// Item is in the middle of the stack
			else if (index < stack.Count - 1)
			{
				stack[index + 1].Bind(stack[index - 1]);
			}
			stack.RemoveAt(index);
			return true;
		}

		public void Clear()
		{
			stack.Clear();
		}

		[OnDeserialized]
		private void Rebind(StreamingContext context)
		{
			// Rebind modifiers after deserialization
			// The first modifier must be bound to Target which may not be set yet at deserialization
			// Set others and rely on Bind() method being called from owner
			for (int i = 1; i < stack.Count; i++)
			{
				stack[i].Bind(stack[i - 1]);
			}
		}

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
