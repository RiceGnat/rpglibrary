using System;
using System.Collections.Generic;
using Davfalcon.Collections.Adapters;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Weapon : Equipment, IWeapon
	{
		public Enum WeaponType { get; set; }
		public int BaseDamage { get; set; }
		public Enum BonusDamageStat { get; set; }
		public int CritMultiplier { get; set; }
		private ManagedEnumStringList damageTypes;
		IEnumerable<Enum> IDamageSource.DamageTypes => damageTypes.ReadOnly;

		private EffectList effects = new EffectList();
		public IEffectList OnHitEffects => effects;
		IEnumerable<IEffectArgs> IEffectSource.Effects => effects.ReadOnly;
		string IEffectSource.SourceName => Name;

		private Weapon(Enum equipmentSlot, Enum type)
			: base(equipmentSlot)
		{
			WeaponType = type;
		}

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
				Weapon = new Weapon(slot, type)
				{
					CritMultiplier = 1,
					damageTypes = new ManagedEnumStringList()
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
				Weapon.damageTypes.Add(type);
				return this;
			}

			public Builder AddDamageTypes(params Enum[] types)
			{
				Weapon.damageTypes.AddRange(types.ConvertEnumArray());
				return this;
			}

			public Builder SetCritMultiplier(int crit)
			{
				Weapon.CritMultiplier = crit;
				return this;
			}

			new public IWeapon Build()
				=> Weapon;
		}
	}
}
