namespace RPGLibrary
{
	public static class ObjectID
	{
		private static uint last = 0;

		public static uint Next
		{
			get { return last++; }
		}
	}
}
