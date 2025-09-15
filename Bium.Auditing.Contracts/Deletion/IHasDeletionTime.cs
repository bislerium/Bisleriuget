using System;

namespace Bium.Auditing.Contracts.Deletion
{
    /// <summary>
    /// Defines a contract for entities that track their deletion timestamp 
    /// and support soft deletion.  
    /// This allows entities to be marked as deleted while retaining historical information.
    /// </summary>
    /// <typeparam name="TDateTime">
    /// The type used to represent the deletion timestamp 
    /// (e.g., <see cref="DateTime"/>, <see cref="DateTimeOffset"/>).
    /// </typeparam>
    public interface IHasDeletionTime<TDateTime> : ISoftDeletable
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the date and time when the entity was deleted.  
        /// Returns <c>null</c> if the entity has not been deleted.
        /// </summary>
        TDateTime? DeletedAt { get; set; }
    }
}