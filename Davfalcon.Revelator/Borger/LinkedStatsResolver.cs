namespace Davfalcon.Revelator.Borger
{
	public class LinkedStatsResolver : LinkedStatsResolverBase
	{
		public const int BASE_ATTRIBUTE = 5;
		public const int BASE_HIT = 100;
		public const int BASE_EVADE = 0;

		private int AdjustAttribute(Attributes stat)
		{
			return Stats[stat] - BASE_ATTRIBUTE;
		}

		public override bool Get(string stat, out int value)
		{
			bool found = true;
			if (stat == CombatStats.HP.ToString())
			{
				value = 25 * Stats[Attributes.VIT];
			}
			else if (stat == CombatStats.MP.ToString())
			{
				value = 5 * Stats[Attributes.INT] + 5 * Stats[Attributes.WIS];
			}
			else if (stat == CombatStats.ATK.ToString())
			{
				value = 2 * AdjustAttribute(Attributes.STR);
			}
			else if (stat == CombatStats.DEF.ToString())
			{
				value = AdjustAttribute(Attributes.VIT) + AdjustAttribute(Attributes.STR);
			}
			else if (stat == CombatStats.MAG.ToString())
			{
				value = 2 * AdjustAttribute(Attributes.INT);
			}
			else if (stat == CombatStats.RES.ToString())
			{
				value = AdjustAttribute(Attributes.INT) + AdjustAttribute(Attributes.WIS);
			}
			else if (stat == CombatStats.HIT.ToString())
			{
				value = BASE_HIT + AdjustAttribute(Attributes.AGI);
			}
			else if (stat == CombatStats.AVD.ToString())
			{
				value = BASE_EVADE + AdjustAttribute(Attributes.AGI);
			}
			else
			{
				value = 0;
				found = false;
			}
			return found;
		}
	}
}
