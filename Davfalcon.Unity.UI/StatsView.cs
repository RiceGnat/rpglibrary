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
			IUnit unit = GetDataAs<IUnit>();

			foreach (AttributeBinding pair in attributes)
			{
				pair.textElement?.Bind(unit.Stats[pair.attribute]);
			}

			foreach (CombatStatsBinding pair in combatStats)
			{
				pair.textElement?.Bind(unit.Stats[pair.stat]);
			}

			base.Draw();
		}
	}
}
