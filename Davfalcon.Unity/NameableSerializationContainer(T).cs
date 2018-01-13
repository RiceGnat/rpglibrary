using RPGLibrary;

namespace Davfalcon.Unity
{
	public class NameableSerializationContainer<T> : SerializationContainer<T> where T : IEditableName
	{
		public override void SerializationPrep()
		{
			obj.Name = name;
		}
	}
}
