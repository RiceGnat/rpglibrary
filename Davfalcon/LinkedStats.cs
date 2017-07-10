﻿using System;
using RPGLibrary;

namespace Davfalcon
{
	public enum Attributes
	{
		STR, VIT, AGI, INT, WIS
	}

	public enum BattleStats
	{
		HP, MP, ATK, DEF, MAG, RES
	}

	public class LinkedStats : StatsMap
	{
		public override int Get(string stat)
		{
			switch ((BattleStats)Enum.Parse(typeof(BattleStats), stat, true))
			{
				case BattleStats.HP:
					return 5 * base.Get(Attributes.VIT);
				case BattleStats.MP:
					return 2 * base.Get(Attributes.INT) + 2 * base.Get(Attributes.WIS);
				case BattleStats.ATK:
					return 2 * base.Get(Attributes.STR);
				case BattleStats.DEF:
					return base.Get(Attributes.VIT) + base.Get(Attributes.STR);
				case BattleStats.MAG:
					return 2 * base.Get(Attributes.INT);
				case BattleStats.RES:
					return base.Get(Attributes.INT) + base.Get(Attributes.WIS);
				default:
					return base.Get(stat);
			}
		}
	}
}
