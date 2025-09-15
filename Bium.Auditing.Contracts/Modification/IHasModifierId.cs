using System;

namespace Bium.Auditing.Contracts.Modification
{
    /// <summary>
    /// Defines a contract for entities that track the identifier of the user 
    /// who last modified them.
    /// </summary>
    /// <typeparam name="TPrimaryKey">
    /// The type of the primary key used to identify the user 
    /// who last modified the entity (e.g., <see cref="int"/>, <see cref="long"/>, <see cref="Guid"/>).
    /// </typeparam>
    public interface IHasModifierId<TPrimaryKey> : IAuditKind
        where TPrimaryKey : struct
    {
        /// <summary>
        /// Gets or sets the primary key of the user who last modified the entity.  
        /// Returns <c>null</c> if the entity has never been modified.
        /// </summary>
        TPrimaryKey? ModifiedBy { get; set; }
    }
}