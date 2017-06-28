﻿using System;
using System.Runtime.Serialization;
using RPGLibrary.Stats;

namespace RPGLibrary
{
	/// <summary>
	/// Implements basic unit functionality.
	/// </summary>
	[Serializable]
	public class BasicUnit : IUnit
	{
		private class BaseStatsRouter : IStatsPackage
		{
			private BasicUnit unit;

			public IStats Base
			{
				get { return unit.BaseStats; }
			}

			public IStats Additions
			{
				get { return StatsConstant.Zero; }
			}

			public IStats Multiplications
			{
				get { return StatsConstant.Zero; }
			}

			public IStats Final
			{
				get { return Base; }
			}

			public BaseStatsRouter(BasicUnit unit)
			{
				this.unit = unit;
			}
		}

		[NonSerialized]
		private BaseStatsRouter statsRouter;

		public uint ID { get; set; }

		public virtual string Name
		{
			get { return Properties.Name; }
			set { Properties.Name = value; }
		}

		public virtual string Class
		{
			get { return Properties.Class; }
			set { Properties.Class = value; }
		}
		public virtual int Level
		{
			get { return Properties.Level; }
			set { Properties.Level = value; }
		}

		public virtual IStats Stats { get { return StatsDetails.Final; } }
		public virtual IStatsPackage StatsDetails { get { return statsRouter; } }

		public IStatsEditable BaseStats { get; protected set; }
		public IUnitModifierStack Modifiers { get; protected set; }
		public IUnitProperties Properties { get; protected set; }

		// Perform initial setup
		protected virtual void Initialize()
		{
			BaseStats = new StatsMap();
			Modifiers = new UnitModifierStack();
			Properties = new UnitProperties();
		}

		// Set internal object references
		protected virtual void Link()
		{
			statsRouter = new BaseStatsRouter(this);
			Modifiers.Bind(this);
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
