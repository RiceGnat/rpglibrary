using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		#region IEnumerable
		IEnumerator<IUnitModifier> IEnumerable<IUnitModifier>.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}
		#endregion
	}
}
