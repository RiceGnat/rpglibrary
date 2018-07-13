namespace Saffron
{
	public interface IDescribable : INameable
	{
		/// <summary>
		/// Gets a description of the object.
		/// </summary>
		string Description { get; }
	}
}
