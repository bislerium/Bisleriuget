namespace Bium.Auditing.Contracts.Deletion
{
    /// <summary>
    /// Defines a contract for entities that support soft deletion.  
    /// Instead of being physically removed from the database, the entity 
    /// is marked as deleted, allowing for recovery or historical tracking.
    /// </summary>
    public interface ISoftDeletable : IAuditKind
    {
        /// <summary>
        /// Gets or sets a value indicating whether the entity is marked as deleted.  
        /// A value of <see langword="true"/> means the entity should be treated 
        /// as deleted, even though it still exists in the data store.
        /// </summary>
        bool IsDeleted { get; set; }
    }
}