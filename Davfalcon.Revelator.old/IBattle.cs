using System.Collections.Generic;

namespace Davfalcon.Revelator
{
	public interface IBattle
	{
		IUnit CurrentUnit { get; }
		IUnitBattleState CurrentUnitState { get; }
		IList<ILogEntry> Log { get; }
		int Turn { get; }
		IList<IUnit> TurnOrder { get; }

		void AddUnit(IUnit unit, int teamId);
		void RemoveUnit(IUnit unit, int teamId);
		IEnumerable<IUnit> GetAllUnits();
		IList<IUnit> GetTeam(int id);
		IUnitBattleState GetUnitState(IUnit unit);
		void Start();
		void NextTurn();
		void End();
	}
}