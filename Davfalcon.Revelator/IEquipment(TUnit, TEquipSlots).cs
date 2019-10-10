using System;

namespace Davfalcon.Revelator
{
    public interface IEquipment<TUnit, TEquipSlots> : IStatsModifier<TUnit>
        where TUnit : IUnitTemplate<TUnit>
        where TEquipSlots : Enum
    {
        string Name { get; }

        string Description { get; }

        TEquipSlots EquipmentSlot { get; }
    }
}
