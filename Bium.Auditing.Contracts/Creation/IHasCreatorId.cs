using System;

namespace Bium.Auditing.Contracts.Creation
{
    /// <summary>
    /// Defines a contract for entities that maintain information about 
    /// the creator's identifier. Typically used in auditing scenarios
    /// to track which user was responsible for creating the entity.
    /// </summary>
    /// <typeparam name="TPrimaryKey">
    /// The type of the primary key used to identify the creator 
    /// (e.g., <see cref="int"/>, <see cref="long"/>, <see cref="Guid"/>).
    /// </typeparam>
    public interface IHasCreatorId<TPrimaryKey> : IAuditKind
        where TPrimaryKey : struct
    {
        /// <summary>
        /// Gets or sets the identifier of the user who created the entity.
        /// </summary>
        TPrimaryKey CreatedBy { get; set; }
    }
}