﻿using Davfalcon.Revelator.Engine;
using UnityEngine;

namespace Davfalcon.Revelator.Unity
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