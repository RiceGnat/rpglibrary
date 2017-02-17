using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using RPGLibrary.Stats;

namespace RPGLibrary
{
	/// <summary>
	/// Implements basic unit functionality.
	/// </summary>
	[Serializable]
	public class BasicUnit : IUnit
	{
		private class UnitStatsRouter : IStatsPackage
		{
			private BasicUnit unit;

			/* This is incomplete. The point is to be able to access both the base stats
			 * with no modifiers as well as the calculated stats with all modifiers.
			 * Once the framework for unit modifiers is in place, the Final property should
			 * be updated to return the calculated stats.
			 */

			public IStats Base
			{
				get { return unit.BaseStats; }
			}

			public IStats Additions
			{
				get { return null; }
			}

			public IStats Multiplications
			{
				get { return null; }
			}

			public IStats Final
			{
				get { return null; }
			}

			public UnitStatsRouter(BasicUnit unit)
			{
				this.unit = unit;
			}
		}

		[NonSerialized]
		private UnitStatsRouter statsRouter;

		public virtual string Name { get; set; }
		public virtual string Class { get; set; }
		public virtual int Level { get; set; }

		public virtual IStats Stats { get { return StatsBreakdown.Final; } }
		public virtual IStatsPackage StatsBreakdown { get { return statsRouter; } }

		public IStatsEditable BaseStats { get; set; }

		// Perform initial setup
		protected virtual void Initialize()
		{
			BaseStats = new StatsMap();
		}

		// Set internal object references
		protected virtual void Link()
		{
			statsRouter = new UnitStatsRouter(this);
		}

		[OnDeserialized]
		private void Rebind(StreamingContext context)
		{
			// Reset object references after deserialization
			Link();
		}

		public BasicUnit()
		{
			Initialize();
			Link();
		}
	}
}
