using RPGLibrary;
using UnityEngine.UI;

namespace Davfalcon.Unity.UI
{
	public class UnitView : DataBoundElement
	{
		public UnitTemplate unitTemplate = null;
		public Text unitName = null;
		public Text unitClass = null;
		public Text unitLevel = null;
		public StatsView statsView = null;
		public UnitEquipmentView equipmentView = null;

		public override void Draw()
		{
			if (unitTemplate != null) Bind(unitTemplate.obj);
			IUnit unit = GetDataAs<IUnit>();

			if (unitName != null) unitName.text = unit.Name;
			if (unitClass != null) unitClass.text = unit.Class;
			if (unitLevel != null) unitLevel.text = unit.Level.ToString();

			statsView?.Bind(unit.Stats);
			equipmentView?.Bind(unit.Properties.GetAs<IUnitItemProperties>());

			base.Draw();
		}
	}
}
