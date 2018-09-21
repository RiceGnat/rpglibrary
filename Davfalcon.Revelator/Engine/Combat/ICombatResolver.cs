using System;
using System.Collections.Generic;

namespace Davfalcon.Revelator.Engine.Combat
{
	public interface ICombatResolver
	{
		void Initialize(IUnit unit);
		void Cleanup(IUnit unit);
		void ApplyBuff(IUnit unit, IBuff buff, string source = null);
		void RemoveBuff(IUnit unit, IBuff buff);
		IList<ILogEntry> Upkeep(IUnit unit);
		HitCheck CheckForHit(IUnit unit, IUnit target);
		Damage CalculateOutgoingDamage(IUnit unit, IDamageSource source, bool scale = true, bool crit = false);
		int CalculateReceivedDamage(IUnit unit, Damage damage);
		IEnumerable<PointLoss> ReceiveDamage(IUnit unit, Damage damage);
		int AdjustVolatileStat(IUnit unit, Enum stat, int change);
		AttackAction Attack(IUnit unit, IUnit target, IWeapon weapon);
		SpellAction Cast(IUnit unit, ISpell spell, params IUnit[] targets);
		SpellAction Cast(IUnit unit, ISpell spell, SpellCastOptions options, params IUnit[] targets);
		IList<ILogEntry> UseItem(IUnit unit, ISpellItem item, params IUnit[] targets);
		IList<ILogEntry> UseItem(IUnit unit, IUsableItem item, params IUnit[] targets);
	}
}
