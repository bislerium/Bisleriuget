using System;

namespace Bium.Auditing.Contracts.Modification
{
    /// <summary>
    /// Defines a contract for entities that track the timestamp of their last modification.
    /// </summary>
    /// <typeparam name="TDateTime">
    /// The type used to represent the modification timestamp 
    /// (e.g., <see cref="DateTime"/>, <see cref="DateTimeOffset"/>).
    /// </typeparam>
    public interface IHasModificationTime<TDateTime> : IAuditKind
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the date and time when the entity was last modified.  
        /// Returns <c>null</c> if the entity has never been modified.
        /// </summary>
        TDateTime? ModifiedAt { get; set; }
    }
}