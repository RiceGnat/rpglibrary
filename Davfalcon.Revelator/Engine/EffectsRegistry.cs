using System.Collections.Generic;

namespace Davfalcon.Revelator.Engine
{
	public class EffectsRegistry : IEffectsRegistry
	{
		private Dictionary<string, Effect> effects = new Dictionary<string, Effect>();

		public IEnumerable<string> Names => effects.Keys;

		public void Register(Effect function, string name)
			=> effects.Add(name, function);

		public Effect Get(string name)
			=> effects[name];

		public IList<ILogEntry> ApplyEffects(IEffectSource source, IUnit target, IUnit originator, int value)
		{
			List<ILogEntry> effects = new List<ILogEntry>();
			foreach (IEffectArgs definition in source.Effects)
			{
				ILogEntry log = Get(definition.Name).Invoke(definition, target, source, originator, value);
				if (log != null) effects.Add(log);
			}
			return effects;
		}

		public IList<ILogEntry> ApplyEffects(IEffectSource source, IUnit target, IUnit originator)
			=> ApplyEffects(source, target, originator, 0);
	}
}
