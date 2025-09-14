using Bisleriuget.Auditing.Contracts.Entity;

namespace Bisleriuget.Auditing.Contracts.Deletion
{
    public interface IHasDeleter<TUser, TPrimaryKey> : IHasDeleterId<TPrimaryKey>
        where TUser : class, IEntity<TPrimaryKey>
        where TPrimaryKey : struct
    {
        /// <summary>
        /// Gets or sets the user entity that deleted the entity.
        /// </summary>
        TUser? Deleter { get; set; }
    }
}