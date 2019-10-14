namespace Davfalcon.Buffs
{
	public interface IUnitBuffManager<TUnit, TBuff> : IModifier<TUnit>
		where TUnit : IUnitTemplate<TUnit>
		where TBuff : IBuff<TUnit>
	{
		TBuff[] GetAllBuffs();
		void Add(TBuff buff);
		void Remove(TBuff buff);
		void RemoveAt(int index);
	}
}
