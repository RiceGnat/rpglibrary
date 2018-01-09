﻿using System;

namespace Davfalcon.Engine
{
	public class SystemData
	{
		public static SystemData Current { get; private set; } = new SystemData();
		public static void SetSystem(SystemData system)
		{
			Current = system ?? throw new ArgumentNullException("System object cannot be set to null.");
		}

		public IEffectFactory Effects { get; private set; } = new EffectFactory();
		public IAutoCatalog<IBuff> Buffs { get; private set; } = new Catalog<IBuff>();
		public IAutoCatalog<ISpell> Spells { get; private set; } = new Catalog<ISpell>();
		public IWeaponFactory Weapons { get; private set; } = new WeaponFactory();
		public IAutoCatalog<IEquipment> Equipment { get; private set; } = new Catalog<IEquipment>();
		public IAutoCatalog<IUsableItem> Items { get; private set; } = new Catalog<IUsableItem>();
	}
}
