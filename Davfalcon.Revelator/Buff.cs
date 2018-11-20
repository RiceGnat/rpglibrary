using System;
using System.Collections.Generic;
using Davfalcon.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public abstract class Buff<T> : UnitModifierBase<T>, IBuff<T> where T : IUnit
	{
		[NonSerialized]
		private IUnit source;
		public IUnit Owner
		{
			get => source;
			set => source = value;
		}

		public bool IsDebuff { get; protected set; }

		public ManagedList<IEffect> Effects { get; } = new ManagedList<IEffect>();
		public int Duration { get; protected set; }
		public int Remaining { get; protected set; }

		IEnumerable<IEffect> IEffectSource.Effects => Effects.AsReadOnly();

		public void Reset()
		{
			Remaining = Duration;
		}

		public bool Tick()
		{
			if (Duration > 0 && Remaining > 0)
			{
				Remaining--;
			}

			return Duration == 0 || Remaining > 0;
		}

	}

	[Serializable]
	public class Buff : Buff<IUnit>, IBuff
	{
		protected override IUnit GetAsTargetInterface() => this;

		public class Builder
		{
			protected Buff buff;

			public Builder()
				=> Reset();

			public Builder Reset()
			{
				buff = new Buff();
				return this;
			}

			public Builder SetName(string name)
			{
				buff.Name = name;
				return this;
			}

			public Builder SetDuration(int duration)
			{
				buff.Duration = duration;
				return this;
			}

			public Builder SetDebuff(bool isDebuff = true)
			{
				buff.IsDebuff = isDebuff;
				return this;
			}

			public Builder SetStatAddition(Enum stat, int value)
			{
				buff.Additions[stat] = value;
				return this;
			}

			public Builder SetStatMultiplier(Enum stat, int value)
			{
				buff.Multipliers[stat] = value;
				return this;
			}

			public Builder AddUpkeepEffect(IEffect effect)
			{
				buff.Effects.Add(effect);
				return this;
			}

			public IBuff Build()
				=> buff;
		}
	}
}
