﻿using System.Collections.Generic;

namespace RPGLibrary
{
	/// <summary>
	/// Exposes methods to manage modifiers.
	/// </summary>
	public interface IUnitModifierStack : IUnitModifier, IEnumerable<IUnitModifier>
	{
		/// <summary>
		/// Gets the number of modifiers in this stack.
		/// </summary>
		int Count { get; }

		/// <summary>
		/// Adds a modifier to this stack.
		/// </summary>
		/// <param name="item">The modifier to be added.</param>
		void Add(IUnitModifier item);

		/// <summary>
		/// Removes a modifier from this stack.
		/// </summary>
		/// <param name="item">The modifier to be removed.</param>
		/// <returns>True if the modifier was found; false otherwise.</returns>
		bool Remove(IUnitModifier item);

		/// <summary>
		/// Removes all modifiers from this stack.
		/// </summary>
		void Clear();
	}
}
