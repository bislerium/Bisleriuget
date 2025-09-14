namespace Bisleriuget.Auditing.Contracts.Deletion
{
    /// <summary>
    /// Represents an entity that tracks its deletion time and supports soft deletion.
    /// </summary>
    /// <typeparam name="TDateTime">The type used to represent the deletion timestamp (e.g., DateTime, DateTimeOffset).</typeparam>
    public interface IHasDeletionTime<TDateTime> : ISoftDeletable
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the date and time when the entity was deleted, or <c>null</c> if the entity has not been deleted.
        /// </summary>
        TDateTime? DeletedAt { get; set; }
    }
}