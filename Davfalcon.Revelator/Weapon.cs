using System;
using System.Collections.Generic;
using Davfalcon.Collections.Generic;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Weapon : Equipment, IWeapon
	{
		public Enum WeaponType { get; set; }
		public int BaseDamage { get; set; }
		public Enum BonusDamageStat { get; set; }
		public int CritMultiplier { get; set; }
		public ManagedList<Enum> DamageTypes { get; set; }
		IEnumerable<Enum> IDamageSource.DamageTypes => DamageTypes.AsReadOnly();

		private EffectList effects = new EffectList();
		public IEffectList OnHitEffects => effects;
		IEnumerable<IEffectArgs> IEffectSource.Effects => effects.ReadOnly;
		string IEffectSource.SourceName => Name;

		private Weapon(Enum slot, Enum type)
			: base(slot)
		{
			WeaponType = type;
		}

		new public class Builder : Equipment.Builder
		{
			private Weapon weapon
			{
				get => equipment as Weapon;
				set => equipment = value;
			}
			private readonly Enum type;

			public Builder(Enum slot, Enum type)
				: base(slot)
			{
				this.type = type;
				Reset();
			}

			new public Builder Reset()
			{
				weapon = new Weapon(slot, type)
				{
					CritMultiplier = 1,
					DamageTypes = new ManagedList<Enum>()
				};
				return this;
			}

			public Builder SetDamage(int baseDamage, Enum bonusDamageStat = null)
			{
				weapon.BaseDamage = baseDamage;
				weapon.BonusDamageStat = bonusDamageStat;
				return this;
			}

			public Builder AddDamageType(Enum type)
			{
				weapon.DamageTypes.Add(type);
				return this;
			}

			public Builder AddDamageTypes(params Enum[] types)
			{
				weapon.DamageTypes.AddRange(types);
				return this;
			}

			public Builder SetCritMultiplier(int crit)
			{
				weapon.CritMultiplier = crit;
				return this;
			}

			new public IWeapon Build()
				=> weapon;
		}
	}
}
