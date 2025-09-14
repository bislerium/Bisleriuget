namespace Bisleriuget.Auditing.Contracts.Modification
{
    /// <summary>
    /// Represents an entity that tracks the last modification time.
    /// </summary>
    /// <typeparam name="TDateTime">The type used to represent the modification timestamp (e.g., DateTime, DateTimeOffset).</typeparam>
    public interface IHasModificationTime<TDateTime> : IAuditKind
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the date and time when the entity was last modified, or <c>null</c> if the entity has not been modified.
        /// </summary>
        TDateTime? ModifiedAt { get; set; }
    }
}