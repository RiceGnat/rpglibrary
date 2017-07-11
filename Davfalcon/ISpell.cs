namespace Davfalcon
{
	public interface ISpell
	{
		Element SpellElement { get; }
		SpellTargetType TargetType { get; }
		int Cost { get; }
		int BaseDamage { get; }
		int BaseHeal { get; }
		int Range { get; }
		int Size { get; }
		int MaxTargets { get; }

		event SpellEventHandler OnCast;
	}
}
