using System;
using System.Collections;
using System.Collections.Generic;

namespace Davfalcon.Collections.Adapters
{
	[Serializable]
	public class ModifierCollectionAdapter<TDerived,TBase> : IModifierCollection<TBase> where TDerived : TBase
	{
		private readonly IModifierCollection<TDerived> collection;

		public ModifierCollectionAdapter(IModifierCollection<TDerived> collection)
			=> this.collection = collection ?? throw new ArgumentNullException(nameof(collection));

		public int Count => collection.Count;
		public TBase AsTargetInterface => (TBase)collection;
		public TBase Target => collection.Target;
		public string Description => collection.Description;
		public string Name => collection.Name;

		public void Bind(TBase target)
		{
			if (target is TDerived t)
				collection.Bind(t);
			else throw new NotSupportedException($"{nameof(target)} is not of the correct type.");
		}

		void IModifier<TBase>.Bind(IModifier<TBase> target) => Bind((TBase)target);

		public void Clear()
			=> throw new NotSupportedException("Cannot modify collection through base interface.");

		void IModifierCollection<TBase>.Add(IModifier<TBase> item)
			=> throw new NotSupportedException("Cannot modify collection through base interface.");

		bool IModifierCollection<TBase>.Remove(IModifier<TBase> item)
			=> throw new NotSupportedException("Cannot modify collection through base interface.");

		void IModifierCollection<TBase>.RemoveAt(int index)
			=> throw new NotSupportedException("Cannot modify collection through base interface.");

		IEnumerator<IModifier<TBase>> IEnumerable<IModifier<TBase>>.GetEnumerator()
			=> throw new NotSupportedException("Cannot enumerate collection through base interface.");

		IEnumerator IEnumerable.GetEnumerator()
			=> throw new NotSupportedException("Cannot enumerate collection through base interface.");
	}
}
