using RPGLibrary;

namespace Davfalcon.Combat
{
	public static class Combat
	{
		public static IUnitCombatProps GetCombatProps(this IUnit unit)
		{
			return unit.Properties.GetAs<IUnitCombatProps>();
		}

		public static int ScaleDamageValue(int baseValue, int scaling)
		{
			return (int)(baseValue * (1 + scaling / 100f));
		}

		public static int MitigateDamageValue(int incomingValue, int resistance)
		{
			return (int)(incomingValue * 100f / (100 + resistance));
		}

		public static Damage CalculateAttackDamage(this IUnit unit)
		{
			IWeapon weapon = unit.GetCombatProps().EquippedWeapon;

			return new Damage(
				DamageType.Physical,
				weapon.AttackElement,
				ScaleDamageValue(weapon.BaseDamage + unit.Stats[Attributes.STR], unit.Stats[CombatStats.ATK]),
				unit.Name
			);
		}

		public static Damage CalculateSpellDamage(this IUnit unit, ISpell spell)
		{
			return new Damage(
				spell.DamageType,
				spell.SpellElement,
				ScaleDamageValue(spell.BaseDamage, unit.Stats[CombatStats.MAG]),
				unit.Name
			);
		}

		public static int CalculateReceivedDamage(this IUnit unit, Damage damage)
		{
			int finalDamage;

			if (damage.Type == DamageType.True)
			{
				finalDamage = damage.Value;
			}
			else {
				CombatStats resistStat;

				if (damage.Type == DamageType.Magical) resistStat = CombatStats.RES;
				else resistStat = CombatStats.DEF;

				finalDamage = MitigateDamageValue(damage.Value, unit.Stats[resistStat]);
			}

			return finalDamage;
		}

		public static HPLoss ReceiveDamage(this IUnit unit, Damage damage)
		{
			int hpLost = unit.CalculateReceivedDamage(damage);

			unit.GetCombatProps().CurrentHP -= hpLost;

			return new HPLoss(
				unit.Name,
				hpLost
			);
		}

		public static AttackAction Attack(this IUnit unit, IUnit target)
		{
			Damage damage = unit.CalculateAttackDamage();

			return new AttackAction(
				unit,
				target,
				damage,
				target.ReceiveDamage(damage)
			);
		}

		public static SpellAction Cast(this IUnit unit, ISpell spell, params IUnit[] targets)
		{
			int n = targets.Length;
			Damage[] damage = new Damage[n];
			HPLoss[] hpLost = new HPLoss[n];

			for (int i = 0; i < n; i++)
			{
				// Damage dealing spells
				if (spell.BaseDamage > 0)
				{
					damage[i] = unit.CalculateSpellDamage(spell);
					hpLost[i] = targets[i].ReceiveDamage(damage[i]);
				}
			}

			return new SpellAction(
				unit,
				spell,
				targets,
				damage,
				hpLost
			);
		}
	}
}
