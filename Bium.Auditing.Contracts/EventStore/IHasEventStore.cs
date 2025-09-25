using System.Collections.Generic;

namespace Bium.Auditing.Contracts.EventStore
{
    /// <summary>
    /// Defines a contract for entities that maintain an event store history.
    /// This interface links an entity to its collection of associated event store entries,
    /// enabling event sourcing and auditing.
    /// </summary>
    public interface IHasEventStore<TStore>
        where TStore : class, IEventStore
    {
        /// <summary>
        /// The collection of event store entries
        /// that capture the history of changes made to the entity.
        /// </summary>
        /// <remarks>
        /// This collection should be treated as an append-only log of events.
        /// Existing events should never be modified or removed,
        /// as they represent the immutable history of the entity.
        /// </remarks>
        ICollection<TStore> EventHistory { get; set; }
    }
}