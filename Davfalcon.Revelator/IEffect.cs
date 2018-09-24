namespace Davfalcon.Revelator
{
	public delegate ILogEntry EffectResolver(EffectArgs args);
	public interface IEffect : INameable
	{
		EffectResolver Resolve { get; }
	}
}
