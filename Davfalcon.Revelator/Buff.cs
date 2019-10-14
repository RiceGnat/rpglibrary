using System;
using Davfalcon.Buffs;

namespace Davfalcon.Revelator
{
	[Serializable]
	public sealed class Buff : Buff<IUnit>, IUnit
	{
		protected override IUnit SelfAsUnit => this;

		public string Source { get; set; }
		public IUnit Owner { get; set; }
	}
}
