using System;
using System.Collections.Generic;
using Davfalcon.Builders;
using Davfalcon.Collections.Adapters;
using Davfalcon.Collections.Generic;
using Davfalcon.Serialization;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Weapon : Equipment, IWeapon
	{
		public Enum WeaponType { get; set; }
		public int BaseDamage { get; set; }
		public Enum BonusDamageStat { get; set; }
		public int CritMultiplier { get; set; } = 1;

		public ManagedEnumStringList DamageTypes { get; } = new ManagedEnumStringList();
		IEnumerable<Enum> IDamageSource.DamageTypes => DamageTypes.AsReadOnly();

		public ManagedList<IEffect> Effects { get; } = new ManagedList<IEffect>();
		IEnumerable<IEffect> IEffectSource.Effects => Effects.AsReadOnly();

		private string owner;
		public string Owner
		{
			get => owner ?? InterfaceUnit.Name;
			set => owner = value;
		}

		protected override IStatsPackage GetStatsResolver()
			=> GetStatsResolver<IWeapon>(this);

		protected Weapon(Enum equipmentSlot, Enum weaponType, IStatsOperations operations)
			: base(equipmentSlot, operations)
		{
			WeaponType = weaponType;
		}

		public static IWeapon Build(Enum equipmentSlot, Enum weaponType, Func<Builder, IBuilder<IWeapon>> builderFunc)
			=> Build(equipmentSlot, weaponType, StatsOperations.Default, builderFunc);

		public static IWeapon Build(Enum equipmentSlot, Enum weaponType, IStatsOperations operations, Func<Builder, IBuilder<IWeapon>> builderFunc)
			=> builderFunc(new Builder(equipmentSlot, weaponType, operations)).Build();

		new public class Builder : EquipmentBuilder<Weapon, IWeapon, Builder>
		{
			private readonly Enum type;

			internal Builder(Enum equipmentSlot, Enum weaponType, IStatsOperations operations)
				: base(equipmentSlot, operations)
			{
				type = weaponType;
				Reset();
			}

			public override Builder Reset()
			{
				build = new Weapon(slot, type, operations);
				return Builder;
			}

			public Builder SetDamage(int baseDamage, Enum bonusDamageStat = null)
			{
				build.BaseDamage = baseDamage;
				build.BonusDamageStat = bonusDamageStat;
				return Builder;
			}

			public Builder AddDamageType(Enum type)
			{
				build.DamageTypes.Add(type);
				return Builder;
			}

			public Builder AddDamageTypes(params Enum[] types)
			{
				build.DamageTypes.AddRange(EnumString.ConvertEnumArray(types));
				return Builder;
			}

			public Builder SetCritMultiplier(int crit)
			{
				build.CritMultiplier = crit;
				return Builder;
			}

			public Builder AddOnHitEffect(IEffect effect)
			{
				build.Effects.Add(effect);
				return Builder;
			}

			public Builder AddOnHitEffect(string name, EffectResolver resolver)
				=> AddOnHitEffect(new Effect(name, resolver));
		}
	}
}
