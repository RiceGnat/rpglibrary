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

			private IStatsDetails TargetDetails => modifier.Target.StatsDetails;

			public IStatsOperations Operations => TargetDetails.Operations;
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
				=> modifier.Multipliers[stat] != Operations.AggregateSeed
					? TargetDetails.GetMultipliersNode(stat).Merge(StatNode<T>.From(modifier, modifier.Multipliers, stat))
					: TargetDetails.GetMultipliersNode(stat);

			public INode GetStatNode(Enum stat)
				=> new ResolverNode($"{stat} ({modifier.Target.Name})",
					GetBaseStatNode(stat),
					GetAdditionsNode(stat),
					GetMultipliersNode(stat),
					Operations);

			public StatsResolver(UnitStatsModifier<T> modifier)
			{
				this.modifier = modifier;

				// Always use default aggregator for addition (sum)
				Additions = new StatsAggregator(TargetDetails.Additions, modifier.Additions);

				// Multiplier aggregation and the final calculation will use the formulas defined by the unit
				Multipliers = new StatsAggregator(TargetDetails.Multipliers, modifier.Multipliers, Operations);
				Final = new StatsCalculator(Base, Additions, Multipliers, Operations);
			}
		}

		[NonSerialized]
		private IStatsDetails statsResolver;

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
			statsResolver = new StatsResolver(this);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UnitModifier{T}"/> class.
		public UnitStatsModifier()
		{
			Additions = new StatsMap();
			Multipliers = new StatsMap();
		}
	}
}
