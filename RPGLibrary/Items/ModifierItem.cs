using System;

namespace RPGLibrary.Items
{
	/// <summary>
	/// Implements basic modifier item properties.
	/// </summary>
	[Serializable]
	public class ModifierItem : UnitStatsModifier, IModifierItem
	{
		IStats IModifierItem.Additions { get { return Additions; } }
		IStats IModifierItem.Multiplications { get { return Multiplications; } }
	}
}
