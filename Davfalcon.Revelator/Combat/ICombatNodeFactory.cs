﻿using System;
using System.Collections.Generic;
using Davfalcon.Nodes;

namespace Davfalcon.Revelator.Combat
{
	public interface ICombatNodeFactory
	{
		INode GetDamageNode(IUnit unit, IDamageSource source);
		INode GetDefenseNode(IUnit defender, INode damage, IEnumerable<Enum> damageTypes);
	}
}
