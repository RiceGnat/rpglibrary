using RPGLibrary;

namespace Davfalcon
{
	public class ClassProperties
	{
		public string DisplayName { get; set; }
		public string Description { get; set; }
		public IEditableStats StatGrowths { get; private set; }

		public ClassProperties()
		{
			StatGrowths = new StatsMap();
		}
	}
}
