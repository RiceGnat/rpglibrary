using System;
using System.Collections.Generic;

namespace Davfalcon
{
	public interface IMathNode : INameable
	{
		int Value { get; }
		object Source { get; }
		Type SourceType { get; }
		IEnumerable<IMathNode> Children { get; }
	}
}
