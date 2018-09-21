namespace Davfalcon.Revelator
{
	public interface ILinkedStatResolver
	{
		bool Get(string stat, out int value);
		void Bind(IUnit unit);
	}
}
