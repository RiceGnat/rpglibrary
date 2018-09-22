using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davfalcon.Revelator
{
	// Incomplete placeholder
	[Serializable]
	public class Effect : IEffect
	{
		public string Name { get; private set; }
		public EffectResolver Resolve { get; private set; }

		public Effect(string name, EffectResolver resolver)
		{
			Name = name;
			Resolve = resolver ?? throw new ArgumentNullException("resolver cannot be null.");
		}
	}
}
