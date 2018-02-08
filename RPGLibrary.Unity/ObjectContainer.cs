namespace UnityEngine
{
	public abstract class ObjectContainer : ScriptableObject
	{
		public abstract T GetObjectAs<T>() where T : class;
	}
}
