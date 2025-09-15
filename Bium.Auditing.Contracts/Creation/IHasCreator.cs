using System;
using Bium.Auditing.Contracts.Entity;

namespace Bium.Auditing.Contracts.Creation
{
    /// <summary>
    /// Defines a contract for entities that maintain a reference to 
    /// the user who created them. Combines the creator's identifier 
    /// (via <see cref="IHasCreatorId{TPrimaryKey}"/>) with a 
    /// navigation property to the user entity.
    /// </summary>
    /// <typeparam name="TUser">
    /// The type of the user entity that created the entity, which must 
    /// implement <see cref="IEntity{TPrimaryKey}"/>.
    /// </typeparam>
    /// <typeparam name="TPrimaryKey">
    /// The type of the primary key used to identify the creator 
    /// (e.g., <see cref="int"/>, <see cref="long"/>, <see cref="Guid"/>).
    /// </typeparam>
    public interface IHasCreator<TUser, TPrimaryKey> : IHasCreatorId<TPrimaryKey>
        where TUser : class, IEntity<TPrimaryKey>
        where TPrimaryKey : struct
    {
        /// <summary>
        /// Gets or sets the user entity that created the entity.
        /// This typically acts as a navigation property in ORMs 
        /// such as Entity Framework.
        /// </summary>
        TUser Creator { get; set; }
    }
}