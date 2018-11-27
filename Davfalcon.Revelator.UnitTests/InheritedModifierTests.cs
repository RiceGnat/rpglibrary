using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Davfalcon.Revelator.UnitTests
{
	[TestClass]
	public class InheritedModifierTests
	{
		private IUnit unit;

		[TestInitialize]
		public void Setup()
		{
			unit = Unit.Build(b => b);
		}

		[TestMethod]
		[ExpectedException(typeof(NotSupportedException))]
		public void BaseInterfaceModifierAdd_NotSupportedException()
		{

		}
	}
}
