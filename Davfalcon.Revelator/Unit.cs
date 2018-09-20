using System;
using System.Runtime.Serialization;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class Unit : BasicUnit, IUnit
	{
		private UnitProperties props;

		public IUnitModifierStack Equipment { get; protected set; }
		public IUnitModifierStack Buffs { get; protected set; }

		public IUnitCombatProperties CombatProperties { get => props as IUnitCombatProperties; }
		public IUnitItemProperties ItemProperties { get => props as IUnitItemProperties; }

		protected override void Initialize()
		{
			BaseStats = new UnitStats(this);
			Modifiers = new UnitModifierStack();

			// Internal references will be maintained after deserialization
			Equipment = new UnitModifierStack();
			Buffs = new UnitModifierStack();
			Modifiers.Add(Equipment);
			Modifiers.Add(Buffs);

			props = new UnitProperties();

			Level = 1;

			// Set base attributes
			foreach (Attributes stat in Enum.GetValues(typeof(Attributes)))
			{
				BaseStats[stat] = UnitStats.BASE_ATTRIBUTE;
			}
		}

		protected override void Link()
		{
			base.Link();
			(BaseStats as UnitStats).Bind(this);
			props.Bind(this);
		}

		[OnSerializing]
		private void SerializationPrep(StreamingContext context)
		{
			// Equipment will be serialized in properties object
			Equipment.Clear();
		}

		public Unit()
			: base()
		{ }

		public Unit(IStatsMath statsMath)
			: base(statsMath)
		{ }
	}
}
