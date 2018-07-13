using Davfalcon.Engine;
using UnityEngine;

namespace Davfalcon.Unity
{
	[ExecuteInEditMode]
	public class SystemDataService : MonoBehaviour
	{
		public static SystemDataService current { get; private set; }

		public IEffectsRegistry effects = new EffectsRegistry();

		void Awake()
		{
			if (current == null) current = this;
			else Destroy(gameObject);
		}
	}
}
