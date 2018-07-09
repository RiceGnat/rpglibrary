using System;
using RPGLibrary.Collections.Generic;

namespace Davfalcon.Engine
{
	// RETHINK this class
	public class SystemData
	{
		public IEffectFactory Effects { get; private set; } = new EffectFactory();
		//public IAutoCatalog<IBuff> Buffs { get; private set; } = new Catalog<IBuff>();
		//public IAutoCatalog<ISpell> Spells { get; private set; } = new Catalog<ISpell>();
		//public IWeaponFactory Weapons { get; private set; } = new WeaponFactory();
		//public IAutoCatalog<IEquipment> Equipment { get; private set; } = new Catalog<IEquipment>();
		//public IAutoCatalog<IUsableItem> Items { get; private set; } = new Catalog<IUsableItem>();


	}
}
