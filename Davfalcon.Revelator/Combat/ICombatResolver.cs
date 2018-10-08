using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator.Combat
{
	public interface ICombatResolver
	{
		ICombatOperations Operations { get; }

		void Initialize(IUnit unit);
		void Cleanup(IUnit unit);
		void ApplyBuff(IUnit unit, IBuff buff, IUnit source = null);
		void RemoveBuff(IUnit unit, IBuff buff);
		HitCheck CheckForHit(IUnit unit, IUnit target);
		IEnumerable<Enum> GetDamageScalingStats(IDamageSource source);
		IEnumerable<Enum> GetDamageScalingStats(IEnumerable<Enum> damageTypes);
		IEnumerable<Enum> GetDamageDefendingStats(IDamageSource source);
		IEnumerable<Enum> GetDamageDefendingStats(IEnumerable<Enum> damageTypes);
		Damage CalculateOutgoingDamage(IUnit unit, IDamageSource source, bool scale = true, bool crit = false);
		int CalculateReceivedDamage(IUnit unit, Damage damage);
		IEnumerable<StatChange> ReceiveDamage(IUnit unit, Damage damage);
		int AdjustVolatileStat(IUnit unit, Enum stat, int change);
		IEnumerable<EffectResult> ApplyEffects(IEffectSource source, IUnit owner, IUnit target, Damage damage = null);

		IEnumerable<EffectResult> Upkeep(IUnit unit);
		ActionResult Attack(IUnit unit, IUnit target, IWeapon weapon);
		ActionResult Cast(IUnit unit, ISpell spell, params IUnit[] targets);
		ActionResult Cast(IUnit unit, ISpell spell, IEnumerable<IUnit> targets, SpellCastOptions options);
	}
}
