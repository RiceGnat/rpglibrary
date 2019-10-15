using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Davfalcon.Buffs;
using Davfalcon.Equipment;

namespace Davfalcon.Revelator
{
	public static class UnitExtensions
	{
		public static IUnitEquipmentManager<IUnit, EquipmentType, IEquipment> GetEquipmentManager(this IUnit unit)
			=> unit.GetComponent<IUnitEquipmentManager<IUnit, EquipmentType, IEquipment>>(UnitComponents.Equipment);

		public static IUnitBuffManager<IUnit, IBuff> GetBuffManager(this IUnit unit)
			=> unit.GetComponent<IUnitBuffManager<IUnit, IBuff>>(UnitComponents.Buffs);

		public static string PrettyPrint(this IUnit unit)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine($"{unit.Name} Lv{unit.Stats[Attributes.LVL]}");

			IList<string> tokens = new List<string>();

			sb.AppendLine("Attributes");

			foreach (Attributes attribute in Enum.GetValues(typeof(Attributes)))
			{
				if (!attribute.Equals(Attributes.LVL))
					tokens.Add($"{Enum.GetName(typeof(Attributes), attribute)} {unit.Stats[attribute]}");
			}

			sb.AppendLine(String.Join(" ", tokens));
			tokens.Clear();

			sb.AppendLine("Combat Stats");

			foreach (CombatStats stat in Enum.GetValues(typeof(CombatStats)))
			{
				tokens.Add($"{Enum.GetName(typeof(CombatStats), stat)} {unit.Stats[stat]}");
			}

			sb.AppendLine(String.Join(" ", tokens));
			tokens.Clear();

			sb.AppendLine();

			sb.AppendLine("Equipment");
			foreach (IEquipmentSlot<EquipmentType, IEquipment> slot in unit.GetEquipmentManager().EquipmentSlots)
			{
				sb.AppendLine($"-{Enum.GetName(typeof(EquipmentType), slot.Type)}: {(slot.IsFull ? slot.Get().Name : "(empty)")}");
			}

			sb.AppendLine();

			sb.AppendLine("Buffs");
			foreach (IBuff buff in unit.GetEquipmentManager().GetAllEquipment()
				.Select(equipment => equipment.GetBuffs())
				.Aggregate((a, b) => a.Union(b).ToArray()))
			{
				sb.AppendLine($"-{buff.Name}");
			}

			return sb.ToString();
		}
	}
}
