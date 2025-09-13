using Bisleriuget.Auditing.Contracts.Entity;

namespace Bisleriuget.Auditing.Contracts.Deletion
{
    /// <summary>
    /// Represents an entity that tracks the deletion time and the deleter's user ID.
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key for the deleter user.</typeparam>
    /// <typeparam name="TDateTime">The type used to represent the deletion timestamp (e.g., DateTime, DateTimeOffset).</typeparam>
    public interface IDeletionAudited<TPrimaryKey, TDateTime> : IHasDeletionTime<TDateTime>
        where TPrimaryKey : struct
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the primary key of the user who deleted the entity.
        /// </summary>
        TPrimaryKey? DeletedBy { get; set; }
    }

    /// <summary>
    /// Represents an entity that tracks the deletion time and the deleter user, along with their ID.
    /// </summary>
    /// <typeparam name="TUser">The type of the user entity associated with the deleter.</typeparam>
    /// <typeparam name="TPrimaryKey">The type of the primary key for the deleter user.</typeparam>
    /// <typeparam name="TDateTime">The type used to represent the deletion timestamp (e.g., DateTime, DateTimeOffset).</typeparam>
    public interface IDeletionAudited<TUser, TPrimaryKey, TDateTime> : IDeletionAudited<TPrimaryKey, TDateTime>
        where TUser : class, IEntity<TPrimaryKey>
        where TPrimaryKey : struct
        where TDateTime : struct
    {
        /// <summary>
        /// Gets or sets the user entity that deleted the entity.
        /// </summary>
        TUser? Deleter { get; set; }
    }
}