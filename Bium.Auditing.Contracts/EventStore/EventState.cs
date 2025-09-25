namespace Bium.Auditing.Contracts.EventStore
{
    /// <summary>
    /// Defines the possible states of an entity in the event store.
    /// </summary>
    public enum EventState
    {
        /// <summary>
        /// The entity was created.
        /// </summary>
        Created = 1,
        /// <summary>
        /// The entity was updated.
        /// </summary>
        Updated = 2,
        /// <summary>
        /// The entity was deleted.
        /// </summary>
        Deleted = 3
    }
}