namespace Davfalcon.Revelator
{
	public delegate void EffectResolver(EffectArgs args);
	public interface IEffect : INameable
	{
		EffectResolver Resolve { get; }
	}
}
