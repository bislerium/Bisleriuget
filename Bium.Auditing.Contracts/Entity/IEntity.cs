using System;

namespace Bium.Auditing.Contracts.Entity
{
    /// <summary>
    /// Defines a contract for entities that have a strongly-typed primary key.
    /// </summary>
    /// <typeparam name="TPrimaryKey">
    /// The type of the primary key for the entity 
    /// (e.g., <see cref="int"/>, <see cref="long"/>, <see cref="Guid"/>).
    /// </typeparam>
    public interface IEntity<TPrimaryKey> : IEntityKind
        where TPrimaryKey : struct
    {
        /// <summary>
        /// Gets or sets the unique identifier for the entity.
        /// </summary>
        TPrimaryKey Id { get; set; }
    }
}