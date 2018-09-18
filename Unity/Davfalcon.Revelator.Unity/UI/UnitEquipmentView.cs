using UnityEngine.UI;

namespace Davfalcon.Revelator.Unity.UI
{
	public class UnitEquipmentView : DataBoundElement
	{
		public Text weaponName;
		public Text armorName;
		public Text accessoryName; 

		public override void Draw()
		{
			IUnitItemProperties props = GetDataAs<IUnitItemProperties>();

			if (weaponName != null) weaponName.text = props.GetEquipment(EquipmentType.Weapon).Name;
			if (armorName != null) armorName.text = props.GetEquipment(EquipmentType.Armor).Name;
			if (accessoryName != null) accessoryName.text = props.GetEquipment(EquipmentType.Accessory).Name;

			base.Draw();
		}
	}
}
