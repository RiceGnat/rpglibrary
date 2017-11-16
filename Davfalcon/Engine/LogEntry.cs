namespace Davfalcon.Engine
{
	public class LogEntry : ILogEntry
	{
		private string entry;

		public LogEntry(string message)
		{
			entry = message;
		}

		public override string ToString()
		{
			return entry;
		}
	}
}
