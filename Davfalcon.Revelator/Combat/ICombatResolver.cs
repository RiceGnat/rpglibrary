using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator.Combat
{
	public interface ICombatResolver
	{
		void Initialize(IUnit unit);
		void Cleanup(IUnit unit);
		void ApplyBuff(IUnit unit, IBuff buff, IUnit source = null);
		void RemoveBuff(IUnit unit, IBuff buff);
		HitCheck CheckForHit(IUnit unit, IUnit target);
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
