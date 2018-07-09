namespace RPGLibrary.Collections.Generic
{
	public interface ISelfRegistry<T> : IRegistry<T> where T : INameable
	{
		void Register(T prototype);
	}
}
