using System;
using RPGLibrary;

namespace Davfalcon
{
	public enum Attributes
	{
		STR, VIT, AGI, INT, WIS
	}

	public enum CombatStats
	{
		HP, MP, ATK, DEF, MAG, RES, HIT, AVD, CRT
	}

	[Serializable]
	public class UnitStats : StatsMap
	{
		[NonSerialized]
		private IUnit unit;

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
				return 2 * unit.Stats[Attributes.STR];
			}
			else if (stat == CombatStats.DEF.ToString())
			{
				return unit.Stats[Attributes.VIT] + unit.Stats[Attributes.STR];
			}
			else if (stat == CombatStats.MAG.ToString())
			{
				return 2 * unit.Stats[Attributes.INT];
			}
			else if (stat == CombatStats.RES.ToString())
			{
				return unit.Stats[Attributes.INT] + unit.Stats[Attributes.WIS];
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
	}
}
