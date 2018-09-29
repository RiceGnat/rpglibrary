using System;
using System.Collections.Generic;
using Davfalcon.Collections.Adapters;
using Davfalcon.Collections.Generic;
using Davfalcon.Serialization;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Weapon : Equipment, IWeapon
	{
		private string owner;
		public string Owner
		{
			get => owner ?? InterfaceUnit.Name;
			set => owner = value;
		}

		public Enum WeaponType { get; set; }
		public int BaseDamage { get; set; }
		public Enum BonusDamageStat { get; set; }
		public int CritMultiplier { get; set; }

		public ManagedEnumStringList DamageTypes { get; } = new ManagedEnumStringList();
		IEnumerable<Enum> IDamageSource.DamageTypes => DamageTypes.AsReadOnly();

		public ManagedList<IEffect> Effects { get; } = new ManagedList<IEffect>();
		IEnumerable<IEffect> IEffectSource.Effects => Effects.AsReadOnly();

		new public class Builder : BuilderBase<Weapon, IWeapon>
		{
			private readonly Enum slot;
			private readonly Enum type;

			public Builder(Enum equipmentSlot, Enum type)
			{
				this.slot = equipmentSlot;
				this.type = type;
				Reset();
			}

			public Builder Reset()
			{
				build = new Weapon()
				{
					SlotType = slot,
					WeaponType = type,
					CritMultiplier = 1
				};
				return this;
			}

			public Builder SetName(string name)
			{
				build.Name = name;
				return this;
			}

			public Builder SetStatAddition(Enum stat, int value)
			{
				build.Additions[stat] = value;
				return this;
			}

			public Builder SetStatMultiplier(Enum stat, int value)
			{
				build.Multiplications[stat] = value;
				return this;
			}

			public Builder AddBuff(IBuff buff)
			{
				build.GrantedBuffs.Add(buff);
				return this;
			}

			public Builder SetDamage(int baseDamage, Enum bonusDamageStat = null)
			{
				build.BaseDamage = baseDamage;
				build.BonusDamageStat = bonusDamageStat;
				return this;
			}

			public Builder AddDamageType(Enum type)
			{
				build.DamageTypes.Add(type);
				return this;
			}

			public Builder AddDamageTypes(params Enum[] types)
			{
				build.DamageTypes.AddRange(EnumString.ConvertEnumArray(types));
				return this;
			}

			public Builder SetCritMultiplier(int crit)
			{
				build.CritMultiplier = crit;
				return this;
			}

			public Builder AddOnHitEffect(IEffect effect)
			{
				build.Effects.Add(effect);
				return this;
			}

			public Builder AddOnHitEffect(string name, EffectResolver resolver)
				=> AddOnHitEffect(new Effect(name, resolver));
		}
	}
}
