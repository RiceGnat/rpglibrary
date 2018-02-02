using UnityEngine.UI;

namespace Davfalcon.Unity.UI
{
	public class UnitEquipmentView : DataBoundElement
	{
		public Text weaponName;
		public Text armorName;
		public Text accessoryName; 

		public override void Draw()
		{
			IUnitItemProperties props = GetDataAs<IUnitItemProperties>();

			if (weaponName != null) weaponName.text = props.GetEquipment(EquipmentSlot.Weapon).Name;
			if (armorName != null) armorName.text = props.GetEquipment(EquipmentSlot.Armor).Name;
			if (accessoryName != null) accessoryName.text = props.GetEquipment(EquipmentSlot.Accessory).Name;

			base.Draw();
		}
	}
}
