using System;
using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon
{
	[Serializable]
	public class UnitStats : StatsMap
	{
		[NonSerialized]
		private IUnit unit;

		public const int BASE_ATTRIBUTE = 5;
		public const int BASE_HIT = 100;
		public const int BASE_EVADE = 0;

		private int AdjustAttribute(Attributes stat)
		{
			return unit.Stats[stat] - BASE_ATTRIBUTE;
		}

		public override int Get(string stat)
		{
			if (stat == CombatStats.HP.ToString())
			{
				return 25 * unit.Stats[Attributes.VIT];
			}
			else if (stat == CombatStats.MP.ToString())
			{
				return 5 * unit.Stats[Attributes.INT] + 5 * unit.Stats[Attributes.WIS];
			}
			else if (stat == CombatStats.ATK.ToString())
			{
				return 2 * AdjustAttribute(Attributes.STR);
			}
			else if (stat == CombatStats.DEF.ToString())
			{
				return AdjustAttribute(Attributes.VIT) + AdjustAttribute(Attributes.STR);
			}
			else if (stat == CombatStats.MAG.ToString())
			{
				return 2 * AdjustAttribute(Attributes.INT);
			}
			else if (stat == CombatStats.RES.ToString())
			{
				return AdjustAttribute(Attributes.INT) + AdjustAttribute(Attributes.WIS);
			}
			else if (stat == CombatStats.HIT.ToString())
			{
				return BASE_HIT + AdjustAttribute(Attributes.AGI);
			}
			else if (stat == CombatStats.AVD.ToString())
			{
				return BASE_EVADE + AdjustAttribute(Attributes.AGI);
			}
			else
			{
				return base.Get(stat);
			}
		}

		public void Bind(IUnit unit)
		{
			this.unit = unit;
		}

		public UnitStats(IUnit unit)
		{
			Bind(unit);
		}

		public static IEnumerable<string> GetAllStatNames()
		{
			List<string> stats = new List<string>();
			stats.AddRange(Enum.GetNames(typeof(Attributes)));
			stats.AddRange(Enum.GetNames(typeof(CombatStats)));
			return stats;
		}
	}
}
