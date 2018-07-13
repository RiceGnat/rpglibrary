using System;

namespace Saffron
{
	/// <summary>
	/// Implements basic modifier item properties.
	/// </summary>
	[Serializable]
	public class ModifierItem : UnitStatsModifier, IModifierItem
	{
		IStats IStatsModifier.Additions { get { return Additions; } }
		IStats IStatsModifier.Multiplications { get { return Multiplications; } }
	}
}
