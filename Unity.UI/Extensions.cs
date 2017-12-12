namespace UnityEngine.UI
{
	public static class Extensions
	{
		public static void SetX(this Vector2 vector, float newX) => vector.Set(newX, vector.y);
		public static void SetY(this Vector2 vector, float newY) => vector.Set(vector.y, newY);
	}
}
