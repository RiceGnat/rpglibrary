using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon.Engine
{
	internal class EffectFactory : IEffectFactory
	{
		private Dictionary<string, EffectTemplate> templates = new Dictionary<string, EffectTemplate>();

		public void LoadTemplate(string name, EffectTemplate template)
			=> templates.Add(name, template);

		public Effect GetEffect(string name, int value)
			=> templates[name](value);

		public IList<ILogEntry> ApplyEffects(IEffects source, IUnit target, IUnit originator)
		{
			List<ILogEntry> effects = new List<ILogEntry>();
			foreach (KeyValuePair<string, int> effect in source.Effects)
			{
				effects.Add(GetEffect(effect.Key, effect.Value).Invoke(target, source.SourceName, originator));
			}
			return effects;
		}
	}
}
