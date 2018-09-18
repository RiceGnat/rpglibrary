namespace UnityEngine.UI
{
	public static class Extensions
	{
		public static void SetX(this Vector2 vector, float newX) => vector.Set(newX, vector.y);
		public static void SetY(this Vector2 vector, float newY) => vector.Set(vector.y, newY);

		/// <summary>
		/// Used to cache component lookup to avoid unnecessary repeated GetComponent calls
		/// </summary>
		/// <typeparam name="T">Type of the component.</typeparam>
		/// <param name="monoBehaviour">The MonoBehaviour containing the component.</param>
		/// <param name="field">The field to cache the component reference.</param>
		public static T SetFieldAndReturnComponent<T>(this MonoBehaviour monoBehaviour, ref T field) where T : Component
		{
			if (field == null) field = monoBehaviour.GetComponent<T>();
			return field;
		}
	}
}
