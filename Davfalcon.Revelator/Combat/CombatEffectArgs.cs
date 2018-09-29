using System;

namespace Davfalcon.Revelator.Combat
{
	[Serializable]
	public class CombatEffectArgs : EffectArgs
	{
		public ICombatResolver CombatResolver { get; }
		public Damage DamageDealt { get; }
		public EffectResult Result { get; set; }

		public CombatEffectArgs(IEffectSource source, IUnit owner, IUnit target, ICombatResolver combatResolver, Damage damageDealt = default)
			: base(source, owner, target)
		{
			CombatResolver = combatResolver;
			DamageDealt = damageDealt;
		}
	}
}
