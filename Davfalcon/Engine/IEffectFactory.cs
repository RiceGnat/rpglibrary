using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon.Engine
{
	public delegate ILogEntry Effect(IEffectArgs definition, IUnit target, IEffectSource source, IUnit originator, int value);

	public interface IEffectFactory
	{
		IEnumerable<string> Names { get; }
		void LoadEffect(string name, Effect function);
		Effect GetEffect(string name);
		IList<ILogEntry> ApplyEffects(IEffectSource source, IUnit target, IUnit originator);
		IList<ILogEntry> ApplyEffects(IEffectSource source, IUnit target, IUnit originator, int value);
	}
}
