using System.Collections.Generic;
using System.Linq;
using Davfalcon.Items;
using RPGLibrary;
using RPGLibrary.Items;

namespace Davfalcon.UnitManagement
{
	public static class InventoryOperations
	{
		public static IUnitInventoryProperties GetInventoryProperties(this IUnit unit) => unit.Properties.GetAs<IUnitInventoryProperties>();

		public static IEnumerable<IItem> GetAllItems(this IUnit unit)
			=> unit.GetInventoryProperties().Inventory;

		public static IEnumerable<T> GetItemsOfType<T>(this IUnit unit) where T : class, IItem
			=> unit.GetAllItems().Where(item => item is T).Cast<T>();

		public static void AddItem(this IUnit unit, IItem item)
			=> unit.GetInventoryProperties().Inventory.Add(item);

		public static bool RemoveItem(this IUnit unit, IItem item)
			=> unit.GetInventoryProperties().Inventory.Remove(item);

		public static void AddTo(this IItem item, IUnit unit)
			=> unit.AddItem(item);

		public static bool RemoveFrom(this IItem item, IUnit unit)
			=> unit.RemoveItem(item);

		public static void UseOn(this IUsableItem item, IUnit user, params object[] targets)
			=> item.Use(user, targets);
	}
}
