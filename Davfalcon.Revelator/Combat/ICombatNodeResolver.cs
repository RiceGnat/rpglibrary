using System.Collections.Generic;

namespace Davfalcon.Revelator.Combat
{
	public interface ICombatNodeResolver : ICombatResolver
	{
		IDamageNode GetDamageNode(IUnit unit, IDamageSource source);
		IDefenseNode GetDefenseNode(IUnit defender, IDamageNode damage);
		IEnumerable<StatChange> ApplyDamage(IDefenseNode damage);
	}
}
