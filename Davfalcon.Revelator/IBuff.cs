using Davfalcon.Buffs;

namespace Davfalcon.Revelator
{
	public interface IBuff : IBuff<IUnit>
	{
		string Source { get; set; }
		IUnit Owner { get; set; }
	}
}
