using System;
using Bium.Auditing.Contracts.Creation;
using Bium.Auditing.Contracts.Entity;

namespace Bium.Auditing.Contracts.EventStore
{
    /// <summary>
    /// A strongly-typed event store entry with a primary key.
    /// Stores the state changes of an entity for event sourcing.
    /// </summary>
    public interface IEventStore<TEntity, TPrimaryKey> : IEntity<TPrimaryKey>, IEventStore
        where TPrimaryKey : struct
        where TEntity : class, IEntityKind
    {
       /// <summary>
       /// Entity instance associated with this event.
       /// This provides navigation back to the entity that produced the event.
       /// </summary>
       /// <remarks>
       /// The reference is primarily for navigation purposes.  
       /// Consumers should rely on <see cref="IEventStore.Payload"/>  
       /// as the immutable snapshot of the entity's state when the event occurred.
       /// </remarks>
       public TEntity Entity {get; set;}
    }
    
    /// <summary>
    /// Represents the base contract for an event store entry,
    /// describing the kind of entity and the serialized value of its state.
    /// </summary>
    public interface IEventStore : IEntityKind, IHasCreationTime<DateTime>
    {
        /// <summary>
        /// the state of the entity at the time of the event
        /// (e.g., Created, Updated, Deleted).
        /// </summary>
        public EventState EventState { get; set; }
        /// <summary>
        /// The serialized representation of the entity's value
        /// at the time the event was recorded.
        /// </summary>
        public string Payload { get; set; }
    }
}