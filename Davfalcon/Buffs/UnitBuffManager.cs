using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Davfalcon.Buffs
{
	public class UnitBuffManager<TUnit, TBuff> : Modifier<TUnit>,
		IUnitBuffManager<TUnit, TBuff>,
		IUnitComponent<TUnit>
		where TUnit : IUnitTemplate<TUnit>
		where TBuff : class, IBuff<TUnit>
	{
		public TBuff[] GetAllBuffs()
		{
			throw new NotImplementedException();
		}

		public void Add(TBuff buff)
		{
			throw new NotImplementedException();
		}

		public void Remove(TBuff buff)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		void IUnitComponent<TUnit>.Initialize(TUnit unit) => unit.Modifiers.Add(this);

		public override TUnit AsModified()
		{
			throw new NotImplementedException();
		}

		public override void Bind(TUnit target)
		{
			base.Bind(target);
		}
	}
}
