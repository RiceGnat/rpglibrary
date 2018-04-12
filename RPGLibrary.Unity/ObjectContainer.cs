namespace UnityEngine
{
	/// <summary>
	/// Extends ScriptableObject to retrieve a stored object as a given type.
	/// </summary>
	public abstract class ObjectContainer : ScriptableObject
	{
		/// <summary>
		/// Get the stored object as a given type.
		/// </summary>
		/// <typeparam name="T">The type of the object.</typeparam>
		public abstract T GetObjectAs<T>() where T : class;
	}
}
