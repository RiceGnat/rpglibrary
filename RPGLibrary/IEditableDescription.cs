namespace RPGLibrary
{
	public interface IEditableDescription : IEditableName
	{
		/// <summary>
		/// Gets or sets a description of the object.
		/// </summary>
		string Description { get; set; }
	}
}
