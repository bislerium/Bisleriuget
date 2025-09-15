using System;

namespace Bium.Auditing.Contracts.Creation
{
    /// <summary>
    /// Defines a contract for entities that store the timestamp 
    /// of when they were created. Useful in auditing and 
    /// history-tracking scenarios.
    /// </summary>
    /// <typeparam name="TDateTime">
    /// The type used to represent the creation timestamp 
    /// (e.g., <see cref="DateTime"/>, <see cref="DateTimeOffset"/>).
    /// </typeparam>
    public interface IHasCreationTime<TDateTime> : IAuditKind
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the date and time when the entity was created.
        /// This value is typically set automatically during persistence.
        /// </summary>
        TDateTime CreatedAt { get; set; }
    }
}