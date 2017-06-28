using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RPGLibrary
{
	// TODO: rebind stack on deserialization

	[Serializable]
	public class UnitModifierStack : UnitModifier, IUnitModifierStack
	{
		private List<IUnitModifier> stack = new List<IUnitModifier>();

		protected override IUnit InterfaceUnit
		{
			get
			{
				return stack.Count > 0 ? stack[stack.Count - 1] : Base;
			}
		}

		public override void Bind(IUnit target)
		{
			base.Bind(target);
			if (stack.Count > 0) stack[0].Bind(Base);
		}

		public void Add(IUnitModifier item)
		{
			item.Bind(InterfaceUnit);
			stack.Add(item);
		}

		public bool Remove(IUnitModifier item)
		{
			// Search for item by object ID
			int index = stack.FindIndex(X => X.ID == item.ID);

			// Not found
			if (index == -1) return false;

			// Item is first and there is an item above it
			if (index == 0 && stack.Count > 1)
			{
				stack[index + 1].Bind(Base); 
			}
			// Item is in the middle of the stack
			else if (index < stack.Count - 1)
			{
				stack[index + 1].Bind(stack[index - 1]);
			}
			stack.RemoveAt(index);
			return true;
		}

		[OnDeserialized]
		private void Rebind(StreamingContext context)
		{
			// Rebind modifiers after deserialization
			for (int i = 0; i < stack.Count; i++)
			{
				if (i == 0)
				{
					stack[i].Bind(Base);
				}
				else
				{
					stack[i].Bind(stack[i - 1]);
				}
			}
		}

		#region IEnumerable
		private IEnumerator<IUnitModifier> GetEnumerator()
		{
			return stack.GetEnumerator();
		}

		IEnumerator<IUnitModifier> IEnumerable<IUnitModifier>.GetEnumerator()
		{
			return GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
		#endregion
	}
}
