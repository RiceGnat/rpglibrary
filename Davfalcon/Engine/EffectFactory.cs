using System.Collections.Generic;
using RPGLibrary;

namespace Davfalcon.Engine
{
	internal class EffectFactory : IEffectFactory
	{
		private Dictionary<string, Effect> effects = new Dictionary<string, Effect>();
		private Dictionary<string, EffectTemplate> templates = new Dictionary<string, EffectTemplate>();

		public void LoadEffect(string name, Effect function)
			=> effects.Add(name, function);

		public void LoadTemplate(string name, EffectTemplate template)
			=> templates.Add(name, template);

		public Effect GetEffect(string name, object[] args)
			=> args == null ? effects[name] : templates[name](args);

		public IList<ILogEntry> ApplyEffects(IEffectSource source, IUnit target, IUnit originator, int value)
		{
			List<ILogEntry> effects = new List<ILogEntry>();
			foreach (IEffectArgs effect in source.Effects)
			{
				// If effect value is zero, use value supplied in parameters
				ILogEntry log = GetEffect(effect.Name, effect.TemplateArgs).Invoke(target, source, originator, effect.Value == 0 ? value : effect.Value);
				if (log != null) effects.Add(log);
			}
			return effects;
		}

		public IList<ILogEntry> ApplyEffects(IEffectSource source, IUnit target, IUnit originator)
			=> ApplyEffects(source, target, originator, 0);
	}
}
