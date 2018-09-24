﻿using System;
using System.Collections.Generic;
using Davfalcon.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Buff : TimedModifier, IBuff
	{
		[NonSerialized]
		private IUnit source;
		public IUnit Source
		{
			get => source;
			set => source = value;
		}

		public string Owner { get; set; }
		public bool IsDebuff { get; set; }

		public ManagedList<IEffect> Effects { get; } = new ManagedList<IEffect>();
		IEnumerable<IEffect> IEffectSource.Effects => Effects.AsReadOnly();

		public class Builder
		{
			protected Buff buff;

			public Builder()
				=> Reset();

			public Builder(IBuff buff)
				=> buff = (buff ?? throw new ArgumentNullException("buff cannot be null."))
					as Buff ?? throw new ArgumentException("This builder can only be used with objects of the Buff class.");

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
				buff.Multiplications[stat] = value;
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
