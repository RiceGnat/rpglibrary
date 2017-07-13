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

		public const int BaseAttribute = 5;
		public const int BaseHit = 100;
		public const int BaseEvade = 0;

		private int AdjustAttribute(Attributes stat)
		{
			return unit.Stats[stat] - BaseAttribute;
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
				return BaseHit + AdjustAttribute(Attributes.AGI);
			}
			else if (stat == CombatStats.AVD.ToString())
			{
				return BaseEvade + AdjustAttribute(Attributes.AGI);
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
