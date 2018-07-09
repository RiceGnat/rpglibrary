using System;
using System.Collections.Generic;
using System.Linq;
using RPGLibrary;
using RPGLibrary.Collections.Generic;

namespace Davfalcon.Engine.Combat
{
	[Serializable]
	public class Battle : IBattle
	{
		[Serializable]
		private class UnitState : IUnitBattleState
		{
			public int Team { get; set; }
			public IBattle Battle { get; private set; }

			public UnitState(IBattle battle)
			{
				Battle = battle;
			}
		}

		[NonSerialized]
		private ICombatEvaluator combat;

		private readonly List<ILogEntry> log = new List<ILogEntry>();
		private readonly Dictionary<int, List<IUnit>> teams = new Dictionary<int, List<IUnit>>();
		private readonly Dictionary<int, IList<IUnit>> teamsReadOnly = new Dictionary<int, IList<IUnit>>();
		private readonly CircularLinkedList<IUnit> turnOrder = new CircularLinkedList<IUnit>();

		public IList<ILogEntry> Log { get; private set; }
		public IList<IUnit> TurnOrder { get; private set; }
		public int Turn { get; private set; }
		public IUnit CurrentUnit { get { return turnOrder.Current; } }
		public IUnitBattleState CurrentUnitState { get { return GetUnitState(CurrentUnit); } }

		public void SetCombatEvaluator(ICombatEvaluator combatEval)
		{
			combat = combatEval ?? throw new ArgumentNullException("Combat evaluator cannot be set to null.");
		}

		private void AddTeam(int id)
		{
			List<IUnit> team = new List<IUnit>();
			teams.Add(id, team);
			teamsReadOnly.Add(id, team.AsReadOnly());
		}

		private void RemoveTeam(int id)
		{
			teams.Remove(id);
			teamsReadOnly.Remove(id);
		}

		public void AddUnit(IUnit unit, int teamId)
		{
			if (!teams.ContainsKey(teamId)) AddTeam(teamId);

			teams[teamId].Add(unit);
			combat.GetCombatProperties(unit).BattleState = new UnitState(this)
			{
				Team = teamId
			};

			if (teamId >= 0)
				turnOrder.Add(unit);
		}

		public void RemoveUnit(IUnit unit, int teamId)
		{
			teams[teamId].Remove(unit);
			combat.GetCombatProperties(unit).BattleState = null;

			if (teamId >= 0)
				turnOrder.Remove(unit);
		}

		public IUnitBattleState GetUnitState(IUnit unit)
			=> combat.GetCombatProperties(unit).BattleState;

		public IList<IUnit> GetTeam(int id)
			=> teamsReadOnly[id];

		public IEnumerable<IUnit> GetAllUnits()
			=> teams.SelectMany(team => team.Value);

		public void Start()
		{
			turnOrder.Sort((a, b) => b.Stats[Attributes.AGI].CompareTo(a.Stats[Attributes.AGI]));

			foreach (IUnit unit in GetAllUnits())
			{
				combat.Initialize(unit);
			}
		}

		public void NextTurn()
		{
			Turn++;
			turnOrder.Rotate();
		}

		public void End()
		{
			foreach (IUnit unit in GetAllUnits())
			{
				combat.Cleanup(unit);
			}
		}

		public Battle(ICombatEvaluator combatEval)
		{
			SetCombatEvaluator(combatEval);
			Log = log.AsReadOnly();
			TurnOrder = turnOrder.AsReadOnly();
		}
	}
}
