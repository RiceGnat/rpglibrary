using System;
using RPGLibrary;
using UnityEngine.UI;

namespace Davfalcon.Unity.UI
{
	public class StatsView : DataBoundElement
    {
		[Serializable]
		public class AttributeBinding
		{
			public Attributes attribute;
			public DataBoundText textElement;
		}

		[Serializable]
		public class CombatStatsBinding
		{
			public CombatStats stat;
			public DataBoundText textElement;
		}

		public AttributeBinding[] attributes;
		public CombatStatsBinding[] combatStats;

		public override void Draw()
		{
			IStats stats = GetDataAs<IStats>();

			if (stats != null)
			{
				foreach (AttributeBinding pair in attributes)
				{
					pair.textElement?.Bind(stats[pair.attribute]);
				}

				foreach (CombatStatsBinding pair in combatStats)
				{
					pair.textElement?.Bind(stats[pair.stat]);
				}
			}

			base.Draw();
		}
	}
}
