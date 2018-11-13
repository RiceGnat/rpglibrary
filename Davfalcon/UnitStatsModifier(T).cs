using System;
using Davfalcon.Nodes;
using Davfalcon.Stats;

namespace Davfalcon
{
	/// <summary>
	/// Modify a unit's stats.
	/// </summary>
	[Serializable]
	public abstract class UnitStatsModifier<T> : UnitModifier<T>, IEditableStatsModifier<T>, IStatsModifier<T>, IUnit where T : IUnit
	{
		private class StatsResolver : IStatsDetails
		{
			private readonly UnitStatsModifier<T> modifier;
			private readonly IStatsOperations operations;

			private IStatsDetails TargetDetails => modifier.Target.StatsDetails;

			public IStats Base => TargetDetails.Base;
			public IStats Additions { get; }
			public IStats Multipliers { get; }
			public IStats Final { get; }

			public INode GetBaseStatNode(Enum stat)
				=> TargetDetails.GetBaseStatNode(stat);

			public IAggregatorNode GetAdditionsNode(Enum stat)
				=> modifier.Additions[stat] != 0
					? TargetDetails.GetAdditionsNode(stat).Merge(StatNode<T>.From(modifier, modifier.Additions, stat))
					: TargetDetails.GetAdditionsNode(stat);

			public IAggregatorNode GetMultipliersNode(Enum stat)
				=> modifier.Multipliers[stat] != operations.AggregateSeed
					? TargetDetails.GetMultipliersNode(stat).Merge(StatNode<T>.From(modifier, modifier.Multipliers, stat))
					: TargetDetails.GetMultipliersNode(stat);

			public INode GetStatNode(Enum stat)
				=> new ResolverNode($"{stat} ({modifier.Target.Name})",
					GetBaseStatNode(stat),
					GetAdditionsNode(stat),
					GetMultipliersNode(stat),
					operations);

			public StatsResolver(UnitStatsModifier<T> modifier, IStatsOperations operations)
			{
				this.modifier = modifier;
				this.operations = operations;

				// Always use default aggregator for addition (sum)
				Additions = new StatsAggregator(TargetDetails.Additions, modifier.Additions);
				Multipliers = new StatsAggregator(TargetDetails.Multipliers, modifier.Multipliers, operations);

				// The final calculation will use the defined formula (or default if not specified)
				Final = new StatsCalculator(Base, Additions, Multipliers, operations);
			}
		}

		[NonSerialized]
		private IStatsDetails statsResolver;

		private readonly IStatsOperations operations;

		/// <summary>
		/// Gets or sets the values to be added to each stat.
		/// </summary>
		public IEditableStats Additions { get; set; }

		/// <summary>
		/// Gets or sets the values to be multiplied with each stat.
		/// </summary>
		public IEditableStats Multipliers { get; set; }

		IStats IStatsModifier<T>.Additions => Additions;
		IStats IStatsModifier<T>.Multipliers => Multipliers;


		IStats IStatsContainer.Stats => statsResolver.Final;
		IStatsDetails IStatsContainer.StatsDetails => statsResolver;

		public override void Bind(T target)
		{
			base.Bind(target);
			statsResolver = new StatsResolver(this, operations);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UnitStatsModifier"/> class.
		/// </summary>
		public UnitStatsModifier()
			: this(StatsOperations.Default)
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="UnitStatsModifier"/> class with the specified <see cref="IStatsOperations"/>.
		/// </summary>
		/// <param name="operations">The stat operations definition.</param>
		public UnitStatsModifier(IStatsOperations operations)
		{
			Additions = new StatsMap();
			Multipliers = new StatsMap();
			this.operations = operations;
		}
	}
}
