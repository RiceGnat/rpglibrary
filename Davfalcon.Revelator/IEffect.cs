namespace Davfalcon.Revelator
{
	public delegate ILogEntry EffectResolver(IUnit owner, IUnit[] targets);
	public interface IEffect : INameable
	{
		EffectResolver Resolve { get; }
	}
}
