using System.Collections.Generic;

namespace Davfalcon.Revelator.Engine.Combat
{
	public delegate void BuffEventHandler(IUnit unit, IBuff buff);
	public delegate void DamageEventHandler(IUnit unit, Damage damage, int hpLost);

	public interface ICombatEvaluator
	{
		event BuffEventHandler OnBuffApplied;
		event DamageEventHandler OnDamageTaken;

		void ApplyBuff(IUnit unit, IBuff buff, string source = null);
		AttackAction Attack(IUnit unit, IUnit target, IWeapon weapon);
		Damage CalculateOutgoingDamage(IUnit unit, IDamageSource source, bool scale = true, bool crit = false);
		int CalculateReceivedDamage(IUnit unit, Damage damage);
		SpellAction Cast(IUnit unit, ISpell spell, params IUnit[] targets);
		SpellAction Cast(IUnit unit, ISpell spell, SpellCastOptions options, params IUnit[] targets);
		int ChangeHP(IUnit unit, int amount);
		int ChangeMP(IUnit unit, int amount);
		HitCheck CheckForHit(IUnit unit, IUnit target);
		void Cleanup(IUnit unit);
		void Initialize(IUnit unit);
		HPLoss ReceiveDamage(IUnit unit, Damage damage);
		void RemoveBuff(IUnit unit, IBuff buff);
		IList<ILogEntry> Upkeep(IUnit unit);
		IList<ILogEntry> UseItem(IUnit unit, ISpellItem item, params IUnit[] targets);
		IList<ILogEntry> UseItem(IUnit unit, IUsableItem item, params IUnit[] targets);
	}
}