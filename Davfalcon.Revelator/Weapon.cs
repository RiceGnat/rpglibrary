using System;
using System.Collections.Generic;
using Davfalcon.Collections.Adapters;
using Davfalcon.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Weapon : Equipment, IWeapon
	{
		private string owner;
		public string Owner {
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

		new public class Builder : Equipment.Builder
		{
			private Weapon Weapon
			{
				get => equipment as Weapon;
				set => equipment = value;
			}
			private readonly Enum type;

			public Builder(Enum equipmentSlot, Enum type)
				: base(equipmentSlot)
			{
				this.type = type;
				Reset();
			}

			new public Builder Reset()
			{
				Weapon = new Weapon()
				{
					SlotType = slot,
					WeaponType = type,
					CritMultiplier = 1
				};
				return this;
			}

			public Builder SetDamage(int baseDamage, Enum bonusDamageStat = null)
			{
				Weapon.BaseDamage = baseDamage;
				Weapon.BonusDamageStat = bonusDamageStat;
				return this;
			}

			public Builder AddDamageType(Enum type)
			{
				Weapon.DamageTypes.Add(type);
				return this;
			}

			public Builder AddDamageTypes(params Enum[] types)
			{
				Weapon.DamageTypes.AddRange(types.ConvertEnumArray());
				return this;
			}

			public Builder SetCritMultiplier(int crit)
			{
				Weapon.CritMultiplier = crit;
				return this;
			}

			public Builder AddOnHitEffect(IEffect effect)
			{
				Weapon.Effects.Add(effect);
				return this;
			}

			new public IWeapon Build()
				=> Weapon;
		}
	}
}
