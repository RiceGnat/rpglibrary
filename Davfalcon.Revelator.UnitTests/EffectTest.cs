using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Davfalcon.Serialization;

namespace Davfalcon.Revelator.UnitTests
{
	[TestClass]
	public class EffectTest
	{
		[TestMethod]
		public void Serialization()
		{
			IEffect e = new Effect("Test", (unit, targets) =>
			{
				return new LogEntry("effect result");
			});

			IEffect clone = Serializer.DeepClone(e);
			ILogEntry result = clone.Resolve(null, null);

			Assert.AreEqual("effect result", result.ToString());
		}
	}
}
