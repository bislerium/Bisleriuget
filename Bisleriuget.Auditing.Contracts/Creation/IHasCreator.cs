using Bisleriuget.Auditing.Contracts.Entity;

namespace Bisleriuget.Auditing.Contracts.Creation
{
    public interface IHasCreator<TUser, TPrimaryKey> : IHasCreatorId<TPrimaryKey>
        where TUser : class, IEntity<TPrimaryKey>
        where TPrimaryKey : struct
    {
        /// <summary>
        /// Gets or sets the user entity that created the entity.
        /// </summary>
        TUser Creator { get; set; }
    }
}