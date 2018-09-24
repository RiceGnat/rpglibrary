namespace Davfalcon.Revelator
{
	public class EffectArgs
	{
		public IEffectSource Source { get; }
		public IUnit Owner { get; }
		public IUnit Target { get; }

		public EffectArgs(IEffectSource source, IUnit owner, IUnit target)
		{
			Source = source;
			Owner = owner;
			Target = target;
		}
	}
}