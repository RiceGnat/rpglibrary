namespace Davfalcon
{
	public interface IEffectArgs
	{
		string Name { get; }
		object[] TemplateArgs { get; }
		int Value { get; }
	}
}