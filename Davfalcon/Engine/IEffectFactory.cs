using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon.Engine
{
	public delegate ILogEntry Effect(IUnit target, IEffectSource source, IUnit originator, int value);
	public delegate Effect EffectTemplate(object[] args);

	public interface IEffectFactory
	{
		void LoadEffect(string name, Effect function);
		void LoadTemplate(string name, EffectTemplate template);
		Effect GetEffect(string name, object[] args);
		IList<ILogEntry> ApplyEffects(IEffectSource source, IUnit target, IUnit originator);
		IList<ILogEntry> ApplyEffects(IEffectSource source, IUnit target, IUnit originator, int value);
	}
}
