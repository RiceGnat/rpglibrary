namespace Davfalcon
{
    public interface IStatsProperties : IStats
    {
        /// <summary>
        /// Gets the base stats.
        /// </summary>
        IStats Base { get; }

        /// <summary>
        /// Gets the total additive modifiers.
        /// </summary>
        IStats Additions { get; }

        /// <summary>
        /// Gets the total multiplicative modifiers.
        /// </summary>
        IStats Multiplications { get; }
    }
}
