using System;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class LinkedStatsResolverBase : ILinkedStatResolver
	{
		[NonSerialized]
		private IUnit unit;

		protected IStats Stats => unit.Stats;

		public void Bind(IUnit unit)
			=> this.unit = unit;

		public virtual bool Get(string stat, out int value)
		{
			value = 0;
			return false;
		}

		public static ILinkedStatResolver Default = new LinkedStatsResolverBase();
	}
}
