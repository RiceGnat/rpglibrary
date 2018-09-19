using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davfalcon.Revelator.Engine.Combat
{
	public struct CombatEvaluatorConfig
	{
		public IEffectsRegistry Effects { get; private set; }
		public IReadOnlyDictionary<Enum, Enum> DamageScaleMap { get; private set; }
		public IReadOnlyDictionary<Enum, Enum> DamageResistMap { get; private set; }
		public Enum SpellAttackType { get; private set; }
		public Enum HPStat { get; private set; }
		public Enum MPStat { get; private set; }
	}
}
