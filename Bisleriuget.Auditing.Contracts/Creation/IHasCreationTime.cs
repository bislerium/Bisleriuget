namespace Bisleriuget.Auditing.Contracts.Creation
{
    /// <summary>
    /// Represents an entity that tracks its creation time.
    /// </summary>
    /// <typeparam name="TDateTime">The type used to represent the creation timestamp (e.g., DateTime, DateTimeOffset).</typeparam>
    public interface IHasCreationTime<TDateTime> : IAuditKind
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// </summary>
        TDateTime CreatedAt { get; set; }
    }
}