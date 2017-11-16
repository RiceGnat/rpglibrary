using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon.Engine
{
	public delegate ILogEntry Effect(IUnit target, string source, IUnit originator);
	public delegate Effect EffectTemplate(int value);

	public interface IEffectFactory
	{
		void LoadTemplate(string name, EffectTemplate template);
		Effect GetEffect(string name, int value);
		IList<ILogEntry> ApplyEffects(IEffects source, IUnit target, IUnit originator);
	}
}
