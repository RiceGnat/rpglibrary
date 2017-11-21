using System;
using System.Collections.Generic;
using RPGLibrary;
using RPGLibrary.Collections.Generic;

namespace Davfalcon.Engine.Combat
{
	[Serializable]
	public class Battle
	{
		private readonly List<ILogEntry> log = new List<ILogEntry>();
		private readonly Dictionary<int, List<IUnit>> teams = new Dictionary<int, List<IUnit>>();
		private readonly Dictionary<int, IList<IUnit>> teamsReadOnly = new Dictionary<int, IList<IUnit>>();
		private readonly CircularLinkedList<IUnit> turnOrder = new CircularLinkedList<IUnit>();

		public IList<ILogEntry> Log { get; private set; }
		public IList<IUnit> TurnOrder { get; private set; }
		public int Turn { get; private set; }
		public IUnit CurrentUnit { get { return turnOrder.Current; } }

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

		public IList<IUnit> GetTeam(int id)
		{
			return teamsReadOnly[id];
		}

		public void AddUnit(IUnit unit, int teamId)
		{
			if (!teams.ContainsKey(teamId)) AddTeam(teamId);

			teams[teamId].Add(unit);

			if (teamId >= 0)
				turnOrder.Add(unit);
		}

		public void RemoveUnit(IUnit unit, int teamId)
		{
			teams[teamId].Remove(unit);

			if (teamId >= 0)
				turnOrder.Remove(unit);
		}

		public void NextTurn()
		{
			Turn++;
			turnOrder.Rotate();
		}

		public void Start()
		{
			turnOrder.Sort((a, b) => b.Stats[Attributes.AGI].CompareTo(a.Stats[Attributes.AGI]));
			
			foreach (List<IUnit> team in teams.Values)
			{
				foreach (IUnit unit in team)
				{
					unit.Initialize();
				}
			}
		}

		public Battle()
		{
			Log = log.AsReadOnly();
			TurnOrder = turnOrder.AsReadOnly();
		}
	}
}
