using System;

namespace Davfalcon
{
	public abstract class Modifier<T> : IModifier<T>, IEditableDescription
	{
		[NonSerialized]
		private T target;

		public string Name { get; set; }
		public string Description { get; set; }

		public virtual T AsTargetInterface => Target;
		public T Target => target;

		public virtual void Bind(T target) => this.target = target;

		public void Bind(IModifier<T> target) => Bind(target.AsTargetInterface);
	}
}
