using UnityEngine;

namespace Davfalcon.Unity
{
	public abstract class ObjectContainer : ScriptableObject
	{
		public abstract T GetObjectAs<T>() where T : class;
	}
}
