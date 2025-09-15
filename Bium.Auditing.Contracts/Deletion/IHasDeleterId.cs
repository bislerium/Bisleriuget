using System;

namespace Bium.Auditing.Contracts.Deletion
{
    /// <summary>
    /// Defines a contract for entities that track the identifier of the user 
    /// who performed a soft deletion.
    /// </summary>
    /// <typeparam name="TPrimaryKey">
    /// The type of the primary key used to identify the user 
    /// who deleted the entity (e.g., <see cref="int"/>, <see cref="long"/>, <see cref="Guid"/>).
    /// </typeparam>
    public interface IHasDeleterId<TPrimaryKey> : ISoftDeletable
        where TPrimaryKey : struct
    {
        /// <summary>
        /// Gets or sets the primary key of the user who deleted the entity.  
        /// Returns <c>null</c> if the entity has not been deleted.
        /// </summary>
        TPrimaryKey? DeletedBy { get; set; }
    }
}