using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon.Engine
{
	internal class EffectFactory : IEffectFactory
	{
		private Dictionary<string, Effect> effects = new Dictionary<string, Effect>();

		public IEnumerable<string> Names => effects.Keys;

		public void LoadEffect(string name, Effect function)
			=> effects.Add(name, function);

		public Effect GetEffect(string name)
			=> effects[name];

		public IList<ILogEntry> ApplyEffects(IEffectSource source, IUnit target, IUnit originator, int value)
		{
			List<ILogEntry> effects = new List<ILogEntry>();
			foreach (IEffectArgs definition in source.Effects)
			{
				ILogEntry log = GetEffect(definition.Name).Invoke(definition, target, source, originator, value);
				if (log != null) effects.Add(log);
			}
			return effects;
		}

		public IList<ILogEntry> ApplyEffects(IEffectSource source, IUnit target, IUnit originator)
			=> ApplyEffects(source, target, originator, 0);
	}
}
