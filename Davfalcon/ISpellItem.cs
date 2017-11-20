namespace Davfalcon
{
	public interface ISpellItem : IUsableItem
	{
		ISpell Spell { get; }
	}
}
