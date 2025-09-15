using Bium.Auditing.Contracts.Entity;

namespace Bium.Auditing.Contracts.Deletion
{
    /// <summary>
    /// Defines a contract for entities that track the user who performed a soft deletion, 
    /// including a navigation reference to the user entity.
    /// </summary>
    /// <typeparam name="TUser">
    /// The type of the user entity who deleted the entity, which must implement <see cref="IEntity{TPrimaryKey}"/>.
    /// </typeparam>
    /// <typeparam name="TPrimaryKey">
    /// The type of the primary key for the user entity (e.g., <see cref="int"/>, <see cref="long"/>, <see cref="Guid"/>).
    /// </typeparam>
    public interface IHasDeleter<TUser, TPrimaryKey> : IHasDeleterId<TPrimaryKey>
        where TUser : class, IEntity<TPrimaryKey>
        where TPrimaryKey : struct
    {
        /// <summary>
        /// Gets or sets the user entity that performed the soft deletion.  
        /// Returns <c>null</c> if the entity has not been deleted or if the deleter is unknown.
        /// </summary>
        TUser? Deleter { get; set; }
    }
}