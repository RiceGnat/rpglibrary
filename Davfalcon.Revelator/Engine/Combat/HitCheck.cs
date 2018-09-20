using System;

namespace Davfalcon.Revelator.Engine.Combat
{
	[Serializable]
	public struct HitCheck
	{
		public readonly bool IsSet;
		public readonly bool Crit;
		public readonly double HitChance;
		public readonly bool Hit;
		public readonly double CritChance;

		public HitCheck(double hitChance, bool hit, double critChance = 0, bool crit = false)
		{
			HitChance = hitChance;
			Hit = hit;
			CritChance = critChance;
			Crit = crit;
			IsSet = true;
		}

		private HitCheck(bool success)
			: this(0, success)
			=> IsSet = false;

		public static HitCheck Success = new HitCheck(true);
		public static HitCheck Miss = new HitCheck(false);
	}
}
