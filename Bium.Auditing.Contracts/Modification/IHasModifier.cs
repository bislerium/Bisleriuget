using System;
using Bium.Auditing.Contracts.Entity;

namespace Bium.Auditing.Contracts.Modification
{
    /// <summary>
    /// Defines a contract for entities that track the user who last modified them, 
    /// including a navigation reference to the modifier entity.
    /// </summary>
    /// <typeparam name="TUser">
    /// The type of the user entity who last modified the entity, which must implement <see cref="IEntity{TPrimaryKey}"/>.
    /// </typeparam>
    /// <typeparam name="TPrimaryKey">
    /// The type of the primary key for the user entity (e.g., <see cref="int"/>, <see cref="long"/>, <see cref="Guid"/>).
    /// </typeparam>
    public interface IHasModifier<TUser, TPrimaryKey> : IHasModifierId<TPrimaryKey>
        where TUser : class, IEntity<TPrimaryKey>
        where TPrimaryKey : struct
    {
        /// <summary>
        /// Gets or sets the user entity who last modified the entity.  
        /// Returns <c>null</c> if the entity has never been modified or if the modifier is unknown.
        /// </summary>
        TUser? Modifier { get; set; }
    }
}