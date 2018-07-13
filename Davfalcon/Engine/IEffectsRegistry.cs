using System.Collections.Generic;
using Saffron.Collections.Generic;

namespace Davfalcon.Engine
{
	public delegate ILogEntry Effect(IEffectArgs definition, IUnit target, IEffectSource source, IUnit originator, int value);

	public interface IEffectsRegistry : IRegistry<Effect> 
	{
		IEnumerable<string> Names { get; }
		IList<ILogEntry> ApplyEffects(IEffectSource source, IUnit target, IUnit originator);
		IList<ILogEntry> ApplyEffects(IEffectSource source, IUnit target, IUnit originator, int value);
	}
}
