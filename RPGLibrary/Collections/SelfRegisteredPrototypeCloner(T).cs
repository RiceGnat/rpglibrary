namespace RPGLibrary.Collections.Generic
{
	public class SelfRegisteredPrototypeCloner<T> : PrototypeCloner<T>, ISelfRegistry<T> where T : INameable
	{
		public void Register(T prototype) => Register(prototype, prototype.Name);
	}
}
