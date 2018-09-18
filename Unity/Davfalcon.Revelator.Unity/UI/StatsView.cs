using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Davfalcon.Revelator.Unity.UI
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

		[MenuItem("GameObject/UI/Davfalcon.Revelator/Stats view", false, 10)]
		private static void Create(MenuCommand menuCommand)
		{
			EditorHelper.CreateGameObjectWithComponent<StatsView>("Stats view", menuCommand);
		}

		[MenuItem("CONTEXT/StatsView/Bind children by name")]
		private static void BindChildrenByName(MenuCommand menuCommand)
		{
			StatsView view = menuCommand.context as StatsView;

			List<AttributeBinding> attributes = new List<AttributeBinding>();
			List<CombatStatsBinding> combatStats = new List<CombatStatsBinding>();

			foreach (Transform child in view.transform)
			{
				DataBoundText text = child.GetComponent<DataBoundText>();

				if (text != null)
				{
					if (Enum.GetNames(typeof(Attributes)).Contains(text.name))
					{
						AttributeBinding binding = new AttributeBinding();
						binding.attribute = (Attributes)Enum.Parse(typeof(Attributes), text.name, true);
						binding.textElement = text;
						attributes.Add(binding);
					}
					else if (Enum.GetNames(typeof(CombatStats)).Contains(text.name))
					{
						CombatStatsBinding binding = new CombatStatsBinding();
						binding.stat = (CombatStats)Enum.Parse(typeof(CombatStats), text.name, true);
						binding.textElement = text;
						combatStats.Add(binding);
					}
				}
			}

			view.attributes = attributes.ToArray();
			view.combatStats = combatStats.ToArray();
		}
	}
}
