using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPGLibrary;

namespace RPGLibraryUnitTests
{
	[TestClass]
	public class BasicUnitTest
	{
		private const string NAME = "Test unit";
		private const string CLASS = "Test class";
		private const int LEVEL = 1;

		[TestMethod]
		public void ConstructorTest()
		{
			IUnit unit = new BasicUnit
			{
				Name = NAME,
				Class = CLASS,
				Level = LEVEL
			};

			Assert.AreEqual(NAME, unit.Name);
			Assert.AreEqual(CLASS, unit.Class);
			Assert.AreEqual(LEVEL, unit.Level);
			Assert.IsNotNull(unit.Stats);
		}
	}
}
