using System;

namespace Davfalcon
{
	[Serializable]
	public abstract class Modifier<T> : IModifier<T>, IEditableDescription
	{
		[NonSerialized]
		private T target;

		public string Name { get; set; }
		public string Description { get; set; }

		public T Target => target;

		public virtual void Bind(T target) => this.target = target;

		public void Bind(IModifier<T> target)
		{
			if (target is T t)
				Bind(t);
			else throw new ArgumentException($"Target modifier must be of type {typeof(T)}");
		}
	}
}
