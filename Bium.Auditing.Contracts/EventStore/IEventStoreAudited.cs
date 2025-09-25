using Bium.Auditing.Contracts.Creation;
using Bium.Auditing.Contracts.Entity;

namespace Bium.Auditing.Contracts.EventStore
{
    /// <summary>
    /// Represents an audited event store entry that tracks the creator's identifier.
    /// </summary>
    public interface IEventStoreAudited<TEntity, TPrimaryKey> : IEventStore<TEntity, TPrimaryKey>, IHasCreatorId<TPrimaryKey>
        where TPrimaryKey : struct
        where TEntity : class, IEntityKind
    {
    }

    /// <summary>
    /// Represents an audited event store entry that includes a reference to the user entity
    /// who created the event.
    /// </summary>
    public interface IEventStoreAudited<TEntity, TUser, TPrimaryKey> : IEventStore<TEntity, TPrimaryKey>, IHasCreator<TUser, TPrimaryKey>
        where TPrimaryKey : struct
        where TUser : class, IEntity<TPrimaryKey>
        where TEntity : class, IEntityKind
    {
    }
    
    /// <summary>
    /// Represents an audited event store entry that includes a reference to the user entity
    /// who created the event.
    /// </summary>
    public interface IEventStoreAudited<TEntity, TUser, TPrimaryKey, TUserPrimaryKey> : IEventStore<TEntity, TPrimaryKey>, IHasCreator<TUser, TUserPrimaryKey>
        where TPrimaryKey : struct
        where TEntity : class, IEntityKind
        where TUserPrimaryKey : struct
        where TUser : class, IEntity<TUserPrimaryKey>
    {
    }
}