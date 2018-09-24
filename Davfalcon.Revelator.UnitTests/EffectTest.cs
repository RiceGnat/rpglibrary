using Davfalcon.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon.Revelator.UnitTests
{
	[TestClass]
	public class EffectTest
	{
		[TestMethod]
		public void Serialization()
		{
			IEffect e = new Effect("Test", (args) =>
			{
				return new LogEntry("effect result");
			});

			IEffect clone = Serializer.DeepClone(e);
			ILogEntry result = clone.Resolve(null);

			Assert.AreEqual("effect result", result.ToString());
		}
	}
}
