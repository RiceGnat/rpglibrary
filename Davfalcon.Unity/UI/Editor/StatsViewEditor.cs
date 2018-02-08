using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace Davfalcon.Unity.UI.Editor
{
	[CustomEditor(typeof(StatsView))]
	public class StatsViewEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			StatsView view = (StatsView)target;

			if (GUILayout.Button("Bind children by name"))
			{
				List<StatsView.AttributeBinding> attributes = new List<StatsView.AttributeBinding>();
				List<StatsView.CombatStatsBinding> combatStats = new List<StatsView.CombatStatsBinding>();

				foreach (Transform child in view.transform)
				{
					DataBoundText text = child.GetComponent<DataBoundText>();

					if (text != null)
					{
						if (Enum.GetNames(typeof(Attributes)).Contains(text.name))
						{
							StatsView.AttributeBinding binding = new StatsView.AttributeBinding();
							binding.attribute = (Attributes)Enum.Parse(typeof(Attributes), text.name, true);
							binding.textElement = text;
							attributes.Add(binding);
						}
						else if (Enum.GetNames(typeof(CombatStats)).Contains(text.name))
						{
							StatsView.CombatStatsBinding binding = new StatsView.CombatStatsBinding();
							binding.stat = (CombatStats)Enum.Parse(typeof(CombatStats), text.name, true);
							binding.textElement = text;
							combatStats.Add(binding);
						}
					}
				}

				view.attributes = attributes.ToArray();
				view.combatStats = combatStats.ToArray();
			}

			DrawDefaultInspector();
		}
	}
}
